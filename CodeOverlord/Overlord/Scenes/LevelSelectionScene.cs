using System;
using Prime;
using System.IO;
using System.Collections.Generic;
using Prime.UI;
using Microsoft.Xna.Framework;
using Overlord;
using Overlord.Overlord.Scenes;
namespace CodeOverlord.Overlord.Scenes
{
	public class LevelSelectionScene : Scene
	{
		const string Path = "Content/Scripts/Levels/";

		private Dictionary<string, Level> levels = new Dictionary<string, Level>();

		private Panel panel;

		public override void Initialize()
		{
			base.Initialize();

			Console.WriteLine("Level selecion is initialized: " + this.Initialized);

			foreach (var dir in Directory.GetDirectories(Path))
			{
				var name = dir.Substring(dir.LastIndexOf("/") + 1);
				name = dir + '/' + name + ".lua";

				if (File.Exists(name))
				{
					levels.Add(dir, new Level(name));
					// levels[dir] = new Level(name);
				}
			}

			panel = new Panel(Vector2.Zero);

			var title = new Header("Seleção de Nível", AnchorPoint.TopCenter);

			panel.AddChild(title);

			foreach (var pair in levels)
			{
				var bt = new Button(pair.Value.Name, AnchorPoint.Auto);
				bt.OnClick += () => this.Game.ActiveScene = new LevelScene(this, pair.Value);

				panel.AddChild(bt);
			}

			this.AddUI(this.panel);
			panel.GetFocus();
		}
	}
}
