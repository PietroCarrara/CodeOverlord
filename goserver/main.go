package main

import (
	"bufio"
	"fmt"
	"github.com/gorilla/websocket"
	"io"
	"io/ioutil"
	"log"
	"net"
	"net/http"
	"os"
)

var conn net.Conn
var wsConn *websocket.Conn

var running bool = true

func main() {
	var err error

	conn, err = net.Dial("tcp", os.Args[1])
	if err != nil {
		log.Println("Error while connecting:", err)
		return
	}
	defer conn.Close()

	go receive()

	go receiveWS()

	static := http.FileServer(http.Dir("."))

	// Static files
	http.Handle("/", static)
	// Websockets upgrade
	http.HandleFunc("/ws", upgrade)
	go http.ListenAndServe(os.Args[2], nil)

	for running {
		// Wait to shutdown...
	}
}

// Receives a websocket upgrade request and upgrades
func upgrade(w http.ResponseWriter, r *http.Request) {

	if wsConn != nil {
		log.Println("More than one websocket client detected! Ignoring...")
		return
	}

	// Upgrade using default values in the documentation
	upgrader := websocket.Upgrader{
		ReadBufferSize:  1024,
		WriteBufferSize: 1024,
	}

	var err error
	wsConn, err = upgrader.Upgrade(w, r, nil)
	if err != nil || wsConn == nil {
		log.Println("Couldn't upgrade to ws:", err)
		running = false
		return
	}
}

func receiveWS() {

	for {

		if wsConn == nil {
			continue
		}

		t, m, err := wsConn.ReadMessage()
		if err != nil {
			log.Println("Error receiving ws message:", err)
			running = false
			return
		}

		switch t {
		case websocket.TextMessage:
			onReceiveWS(string(m))
		case websocket.CloseMessage:
			wsConn = nil
		default:
			log.Println("Message of unsupported type:", t)
		}

	}
}

// Wait for C# to send messages, and raise onReceive
func receive() {
	for {
		bytes, _ := ioutil.ReadAll(conn)
		str := string(bytes)
		if err != nil {

			// Terminate once the connection has closed
			// if err == io.EOF {
			//	running = false
			//	return
			//}

			log.Println("Error when receiving data: ", err)
		}

		// Remove the last char, a \0
		onReceive(string(str[:len(str)-1]))
	}
}

func onReceive(str string) {
	log.Println("Sending string C#->JS")
	sendWS(str)
}

func onReceiveWS(str string) {
	log.Println("Sending string JS->C#")
	send(str)
}

// Sends a message to C#
func send(str string) {
	_, err := fmt.Fprintf(conn, "%s", str)

	if err != nil {
		log.Println(err)
	}
}

func sendWS(str string) {
	err := wsConn.WriteMessage(websocket.TextMessage, []byte(str))

	if err != nil {
		log.Println("Error while sending message to ws:", err)
		running = false
	}
}
