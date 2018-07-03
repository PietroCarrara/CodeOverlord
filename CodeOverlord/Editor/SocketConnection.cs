using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading.Tasks;

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

		public void Start()
		{
			conn = new Socket(SocketType.Stream, ProtocolType.Tcp);
			running = true;

			conn.Bind(new IPEndPoint(IPAddress.Any, SockPort));
			conn.Listen(1);

			var exe = "";

			// Check if is running on windows
			int p = (int) Environment.OSVersion.Platform;
			if ((p != 4) && (p != 6) && (p != 128))
			{
				exe = ".exe";
			}

			Process.Start("goserver" + exe, ":" + SockPort + " :" + HttpPort);

			conn = conn.Accept();

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

		public void Send(string s)
		{
			conn.Send(Encoding.UTF8.GetBytes(s + "\0"));
		}

		public Task SendAsync(string s)
		{
			return Task.Run(() => Send(s));
		}

		private void receive()
		{
			try
			{
				while(running)
				{
					if (conn.Available > 0)
					{
						var bytes = new byte[conn.Available];

						conn.Receive(bytes);

						var str = Encoding.UTF8.GetString(bytes);

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
