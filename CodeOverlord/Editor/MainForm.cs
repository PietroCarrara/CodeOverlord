using System;
using System.IO;
using Prime;
using Eto.Forms;
using Eto.Drawing;
using System.Web;
using System.Threading.Tasks;

namespace Overlord.Editor
{
	public class MainForm : Form
	{
		static WebView web = new WebView();

		public MainForm()
		{
			this.Title = "Code Overlord | Text Editor";

			this.ClientSize = new Size(800, 600);

			var file = Directory.GetCurrentDirectory() + "/Index.html";

			web.Url = new System.Uri("file://" + file);

			this.Content = web;

			web.DocumentLoading += handleCall;
		}

		public void Save(string name, string contents)
		{
			App.SaveScript(name, contents);
		}

		public void Alert(string text)
		{
			text = escape(text);

			web.ExecuteScript("alert('"+ text +"');");
		}

		public void handleCall(object sender, WebViewLoadingEventArgs arg)
		{
			if (arg.Uri.Scheme != "action")
				return;

			arg.Cancel = true;
			
			switch(arg.Uri.Host)
			{
				case "save":
					saveScript();
					break;
				case "done":
					alert("Editor Ready, Sir!");
					break;
			}
		}

		// TODO: Implement
		public void CreateSession(string name, string code)
		{
			// web.ExecuteScript("createSession(" + escape(name) + ", " + escape(code) + ")");
		}

		public void SetSession(string name)
		{
			// web.ExecuteScript("setSession(" + escape(name) + ")");
		}

		private string escape(string text)
		{
			return text = text.Replace("\\", "\\\\").Replace("\n", "\\n").Replace("\'", "\\'").Replace("\"", "\\\"");
		}

		public string Text
		{
			get
			{
				return web.ExecuteScript("return editor.getValue();");
			}
			set
			{
				web.ExecuteScript("editor.setValue('" + escape(value) + "');");
			}
		}

		private void alert(string text)
		{
			var t = new Task(() => App.Alert(text));
			t.Start();
		}

		private void saveScript()
		{
			// TODO: Create sessions (tabs) and restore their names
			var t = new Task(() => App.SaveScript("sample-slime.lua", App.GetText()));
			t.Start();
		}
	}
}
