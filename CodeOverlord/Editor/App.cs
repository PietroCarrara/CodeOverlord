using Eto.Forms;
using System;
using System.Threading.Tasks;

namespace Overlord.Editor
{
	public static class App
	{
		public static Application app;

		public static MainForm form;

		public static OverLordGame Game;

		public static void Run()
		{
			app.Run(form);
		}

		public static void Quit()
		{
			app.Quit();
		}

		public static void OnEditorReady()
		{
			Game.OnEditorReady();
		}

		public static string GetText()
		{
			var text = "";

			app.Invoke(() =>
			{
				text = form.Text;
			});

			return text;
		}

		public static void CreateSession(string name, string code)
		{
			app.Invoke(() => form.CreateSession(name, code));
		}

		public static void SetSession(string name)
		{
			app.Invoke(() => form.SetSession(name));
		}

		public static void SaveScript(string name, string content)
		{
			Game.SetScriptText(name, content);
		}

		public static void SetText(string text)
		{
			app.Invoke(() =>
			{
				form.Text = text;
			});
		}

		public static void Alert(string text)
		{
			app.Invoke(() =>
			{
				form.Alert(text);
			});
		}
	}
}
