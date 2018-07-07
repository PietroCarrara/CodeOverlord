using System;
using Prime;
using Overlord.Editor;

namespace Overlord
{
	public class OverLordGame : PrimeGame
	{
		public OverLordGame(LevelScene s) : base(s)
		{
			this.Window.AllowUserResizing = true;
			this.IsMouseVisible = false;
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
			var s = this.ActiveScene as LevelScene;

			if (s == null)
				return;

			s.OnEditorReady();
		}
	}
}
