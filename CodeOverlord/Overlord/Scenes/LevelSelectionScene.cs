using System;
using Prime;
using System.IO;
using System.Collections.Generic;
using Prime.UI;
using Microsoft.Xna.Framework;
using Overlord;
using Overlord.Overlord.Scenes;
using System.Linq;
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

			foreach (var dir in Directory.GetDirectories(Path))
			{
				var name = dir.Substring(dir.LastIndexOf("/") + 1);
				name = dir + '/' + name + ".lua";

				if (File.Exists(name))
				{
					levels.Add(dir, new Level(name));
				}
			}

			panel = this.AddUI(new Panel(Vector2.Zero));

			var title = new Header("Seleção de Nível", AnchorPoint.TopCenter);

			panel.AddChild(title);

			foreach (var pair in levels.OrderBy((e) => e.Value.Index))
			{
				var bt = new Button(pair.Value.Name, AnchorPoint.Auto);
				bt.OnClick += () => this.Game.ActiveScene = new LevelScene(this, pair.Value);

				Console.WriteLine("Index: " + pair.Value.Index);

				panel.AddChild(bt);
			}

			this.AddUI(this.panel);
			panel.GetFocus();
		}
	}
}
