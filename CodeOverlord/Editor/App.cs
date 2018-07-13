using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Overlord.Editor
{
	public static class App
	{
		private static SocketConnection conn;

		private static OverLordGame game;

		public static void Run(OverLordGame g)
		{
			game = g;

			conn = new SocketConnection();

			conn.OnReceive = onReceive;

			conn.Start();

			Process.Start("http://localhost:" + SocketConnection.HttpPort + "/Index.html");
		}

		public static void Quit()
		{
			conn.Stop();
		}

		public static void ResetSessions()
		{
			conn.Send("reset,\n");
		}

		// TODO: Remove this, it's not a good desing
		public static void OnEditorReady()
		{
			game.OnEditorReady();
		}

		private static void onReceive(string str)
		{
			var data = str.Split('\n');
			var instructions = data[0].Split(',');
			var dataArgs = "";
			if (data.Length > 1)
			{
				dataArgs = string.Join("\n", data.Skip(1));
			}

			switch (instructions[0])
			{
				case "onReady":
					OnEditorReady();
					break;
				case "save":
					SaveScript(instructions[1], dataArgs);
					break;
			}
		}

		private static bool received;
		private static string text;
		public static string GetText(string session = "")
		{
			text = "";
			received = false;

			if (session != null)
			{
				session = "," + session;
			}

			conn.Send("getText" + session + "\n");

			while (!received)
			{
				// Wait...
			}

			return text;
		}

		// Called when Go gives us the requested code
		private static void receiveText(string txt)
		{
			text = txt;
			received = true;
		}

		public static void CreateSession(string name, string code, bool readOnly)
		{
			var ro = "";
			if (readOnly)
			{
				ro = ",readonly";
			}

			conn.Send("createSession," + name + ro + "\n" + code);
		}

		public static void SetSession(string name)
		{
			conn.Send("setSession," + name + "\n");
		}

		public static void SaveScript(string name, string content)
		{
			game.SetScriptText(name, content);
		}

		public static void SetText(string file, string text)
		{
			string data = "setText," + file + "\n" + text;

			conn.Send(data);
		}

		public static void Alert(string text)
		{
			conn.Send("alert\n" + text);
		}
	}
}
