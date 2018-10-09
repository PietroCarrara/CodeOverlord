using System;
using Prime;
using System.IO;
using System.Collections.Generic;
using Prime.UI;
using Microsoft.Xna.Framework;
using Overlord;
using Overlord.Overlord.Scenes;
using System.Linq;
using CodeOverlord.Overlord.Systems;
namespace CodeOverlord.Overlord.Scenes
{
	public class LevelSelectionScene : Scene
	{
		const string Path = "Content/Scripts/Levels/";

		// The levels and it's buttons
		private Dictionary<string, Level> levels = new Dictionary<string, Level>();
		private Dictionary<string, Button> buttons = new Dictionary<string, Button>();

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

			var player = PlayerData.GetInstance();

			foreach (var pair in levels.OrderBy((e) => e.Value.Index))
			{
				var bt = buttons[pair.Key] = new Button(pair.Value.Name, AnchorPoint.Auto);
				bt.OnClick += () => this.Game.ActiveScene = new LevelScene(this, pair.Value);
				panel.AddChild(bt);

				// The player begins at 0, so he only has one
				// level available
				if (player.MaxIndex < pair.Value.Index - 1)
				{
					bt.Enabled = false;
				}
			}

			this.AddUI(this.panel);
			panel.GetFocus();
		}

		// Checks againg which levels
		// are locked and which are not
		public void ReevalLocks()
		{
			var player = PlayerData.GetInstance();

			foreach (var pair in levels)
			{
				// The player begins at 0, so he only has one
				// level available
				if (player.MaxIndex < pair.Value.Index - 1)
				{
					buttons[pair.Key].Enabled = false;
				}
				else
				{
					buttons[pair.Key].Enabled = true;
				}
			}
		}
	}
}
