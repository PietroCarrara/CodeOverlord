using Prime;
using Prime.Graphics;
using Prime.UI;
using System.Linq;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Overlord.Editor;
using CodeOverlord.Overlord.LuaScripts;
using System;
using Overlord.Overlord.Scenes;

namespace Overlord
{
	public class LevelScene : Scene
	{
		private Entity camTarget;
		private const float camSpeed = 15;

		private Scene parent;

		public Level Level { get; private set; }

		public string Name;

		public TilingMap Map { get; private set; }
		private string map;

		private bool ended = true;

		private bool resetFiles;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Overlord.LevelScene"/> class.
		/// </summary>
		/// <param name="parent">Parent scene to return on completion.</param>
		/// <param name="level">The level object that represents this level.</param>
		/// <param name="resetFiles">If set to <c>true</c>If the files on the browser should be overwritten.</param>
		public LevelScene(Scene parent, Level level, bool resetFiles = true)
		{
			this.parent = parent;
			this.Level = level;
			this.resetFiles = resetFiles;

			VirtualFileScriptLoader.Files = level.Files;

			BattleManager.Init(this);

			this.ClearColor = Color.Blue;
		}

		public override void Initialize()
		{
			base.Initialize();

			this.Map = this.Add(TilingMap.Load(Content, Level.Map));
			this.Map.Width = Level.Width;
			this.Map.Height = Level.Height;

			Map.Position = PrimeGame.Center;

			Grid.Init(this);
			Grid.Width = this.Map.WidthInTiles;
			Grid.Height = this.Map.HeightInTiles;
			Grid.TileWidth = this.Map.TileWidth;
			Grid.TileHeight = this.Map.TileHeight;

			var dragger = new MonsterDragger();
			var spawner = new MonsterSpawner();

			var bt = new Button("Start!", Prime.UI.AnchorPoint.TopCenter, new Vector2(200, 50), new Vector2(0, 100));
			bt.OnClick = () =>
			{
				ended = false;

				BattleManager.Sort();
				Level.Initialize();

				dragger.Destroy();
				spawner.Destroy();
				bt.Unnatach();
			};

			var selector = Add(new ScriptSelector(300, 300, spawner));
			foreach (var pair in Level.Files)
			{
				if (pair.Value.Spawnable)
					selector[pair.Key] = pair.Value;
			}
			selector.IsVisible = false;

			// Camera setup
			camTarget = new Entity
			{
				Position = Vector2.Zero
			};

			this.Cam.Add(new DelayedFollowTarget(camTarget, 10));
			this.Cam.Position = Vector2.Zero;

			var dialog = this.Add(Level.Dialog);

			// Build UI after the dialog is gone
			dialog.OnDone = () =>
			{
				Add(spawner);
				Add(dragger);
				AddUI(bt);
				this.Add(new MousePositionHighlight(this.Map.TileWidth, this.Map.TileHeight));

				if (selector.Files.Any())
					selector.IsVisible = true;
			};
			dialog.Rewind();
			dialog.Next();

			OnEditorReady();

			Level.Ready();
		}

		public void OnEditorReady()
		{
			if (this.resetFiles)
			{
				foreach (var pair in Level.Files)
				{
					Console.WriteLine("Sending " + pair.Key);
					App.CreateSession(pair.Key, pair.Value.Text, pair.Value.ReadOnly);
				}
			}
		}

		public void SetScriptText(string key, string text)
		{
			if (Level.Files.ContainsKey(key) && !Level.Files[key].ReadOnly)
				Level.Files[key].Text = text;
		}

		public void CheckWin()
		{
			var status = Level.Update();

			switch (status)
			{
				case "win":
					win();
					break;
				case "lose":
					lose();
					break;
			}
		}

		private void win()
		{
			ended = true;

			var bt = new Button("You've won! Go back...", AnchorPoint.Center, new Vector2(200, 50));
			bt.OnClick = () =>
			{
				App.ResetSessions();

				this.Game.ActiveScene = parent;
				this.Destroy();
			};

			this.AddUI(bt);
		}

		private void lose()
		{
			ended = true;

			var bt = new Button("You've Lost! Go back...", AnchorPoint.Center, new Vector2(200, 50), new Vector2(0, -100));
			bt.OnClick = () =>
			{
				App.ResetSessions();

				this.Game.ActiveScene = parent;
				this.Destroy();
			};

			var bt2 = new Button("Retry", AnchorPoint.Center, new Vector2(200, 50), new Vector2(0, 100));
			bt2.OnClick = () =>
			{
				this.Game.ActiveScene = new LevelScene(parent, Level, false);
				this.Destroy();
			};

			this.AddUI(bt);
			this.AddUI(bt2);
		}

		public override void Update()
		{
			base.Update();

			if (Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right))
				camTarget.Position.X += camSpeed;
			else if (Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left))
				camTarget.Position.X -= camSpeed;

			if (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down))
				camTarget.Position.Y += camSpeed;
			else if (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up))
				camTarget.Position.Y -= camSpeed;

			// Updates entities, but not the
			// battle manager
			if (ended)
				return;

			BattleManager.Update();
		}

		private Panel console;

		public static void ConsoleWriteLine(string str)
		{
			if (PrimeGame.Game.ActiveScene is LevelScene l)
			{
				l.consoleWriteLine(str);
			}
		}

		public void consoleWriteLine(string str)
		{
			if (console == null)
			{
				console = new Panel(new Vector2(400, 300));
				console.Draggable = true;
				this.AddUI(console);
			}

			console.AddChild(new Label(str));
		}
	}
}
