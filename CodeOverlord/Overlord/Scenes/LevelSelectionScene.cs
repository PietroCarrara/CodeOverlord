using System;
using Prime;
using System.IO;
using System.Collections.Generic;
using Prime.UI;
using Microsoft.Xna.Framework;
using Overlord;
namespace CodeOverlord.Overlord.Scenes
{
	public class LevelSelectionScene : Scene
	{
		const string Path = "Content/Scripts/Levels/";

		private Dictionary<string, string> levels = new Dictionary<string, string>();

		private Panel panel;

		public LevelSelectionScene()
		{
			foreach (var dir in Directory.GetDirectories(Path))
			{
				var name = dir.Substring(dir.LastIndexOf("/") + 1);
				name = dir + '/' + name + ".lua";

				if (File.Exists(name))
				{
					levels.Add(dir, name);
				}
			}
		}

		public override void Initialize()
		{
			base.Initialize();

			panel = new Panel(Vector2.Zero);

			foreach (var pair in levels)
			{
				var bt = new Button(pair.Value, AnchorPoint.Auto);
				bt.OnClick += () => this.Game.ActiveScene = new LevelScene(this, pair.Value, pair.Key + "/");

				panel.AddChild(bt);

			}

			this.AddUI(this.panel);
		}
	}
}
