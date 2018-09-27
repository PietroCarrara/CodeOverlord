using System;
using Prime;
using Overlord.Editor;
using CodeOverlord.Overlord.LuaScripts;
using MoonSharp.Interpreter;
using CodeOverlord.Overlord.Scenes;

namespace Overlord
{
	public class OverLordGame : PrimeGame
	{
		public OverLordGame(Scene s) : base(s)
		{
			this.Window.AllowUserResizing = true;
			this.IsMouseVisible = false;

			Script.DefaultOptions.ScriptLoader = new VirtualFileScriptLoader();
		}

		public void SetScriptText(string sName, string text)
		{
			var s = this.ActiveScene as LevelScene;

			if (s == null)
				return;

			s.SetScriptText(sName, text);
		}

		protected override void Initialize()
		{
			CharManager.Initialize(this.Content);

			base.Initialize();
		}

		public void OnEditorReady()
		{
			if (this.ActiveScene is ConnectionLoadingScene l)
			{
				l.Done();
			}
		}
	}
}
