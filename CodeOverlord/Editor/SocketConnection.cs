using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Prime;

namespace Overlord.Editor
{
	public class SocketConnection
	{
		public const int SockPort = 12346;
		public const int HttpPort = 12347;

		private Socket conn;

		private bool running;

		public Action<string> OnReceive;

		private Task receiveTask;

		private Queue<string> messages = new Queue<string>();

		public void Start()
		{
			conn = new Socket(SocketType.Stream, ProtocolType.Tcp);
			running = true;

			conn.Bind(new IPEndPoint(IPAddress.Any, SockPort));
			conn.Listen(1);

			var exe = "";

			// Check if is running on windows
			int p = (int)Environment.OSVersion.Platform;
			if ((p != 4) && (p != 6) && (p != 128))
			{
				exe = ".exe";
			}

			var pinfo = new ProcessStartInfo("goserver" + exe, ":" + SockPort + " :" + HttpPort);

#if !DEBUG
			pinfo.RedirectStandardOutput = pinfo.RedirectStandardError = true;
			pinfo.UseShellExecute = false;
			pinfo.CreateNoWindow = true;
#endif
			Process.Start(pinfo);

			conn = conn.Accept();

			Task.Run(() => messenger());

			receiveTask = new Task(receive);
			receiveTask.Start();
		}

		public void Stop()
		{
			running = false;

			receiveTask.Wait();

			conn.Shutdown(SocketShutdown.Both);
			conn.Close();
			conn.Dispose();
		}

		private void messenger()
		{
			while (true)
			{
				if (messages.Count > 0)
				{
					var s = messages.Dequeue();
					try
					{
						conn.Send(Encoding.UTF8.GetBytes(s + "\n\0"));

					}
					catch (SocketException e)
					{
						Console.WriteLine("Error while sending message!\nsocket: " + e.Message);
					}
				}
			}
		}

		public void Send(string s)
		{
			messages.Enqueue(s);
			Console.WriteLine("Enfileirando mensagem!");
		}

		private void receive()
		{
			try
			{
				while (running)
				{
					if (conn.Available > 0)
					{
						var bytes = new byte[conn.Available];

						conn.Receive(bytes);

						var str = Encoding.UTF8.GetString(bytes);

						Console.WriteLine("Recebendo mensagem: " + str);

						OnReceive?.Invoke(str);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
