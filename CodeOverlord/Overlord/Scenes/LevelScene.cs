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

			BattleManager.Init(this);

			VirtualFileScriptLoader.Files = level.Files;

			this.ClearColor = Color.Blue;
		}

		public override void Initialize()
		{
			base.Initialize();

			this.Map = this.Add(TilingMap.Load(Content, Level.Map));
			this.Map.Width = Level.Width;
			this.Map.Height = Level.Height;

			var dialog = this.Add(Level.Dialog);

			// Speed controller
			var speedPanel = this.AddUI(new Panel(new Vector2(300, 200), AnchorPoint.TopLeft));
			speedPanel.IsVisible = false;
			speedPanel.AddChild(new Header("Velocidade"));
			speedPanel.Draggable = true;

			var scaleLabel = new Label("1x");
			speedPanel.AddChild(scaleLabel);

			var speed = new Slider(50, 500);
			speed.Value = 100;
			speed.OnValueChange += (s) =>
			{
				var scale = ((Slider)s).Value / 100f;

				TimeControl.SetScale(scale);
				scaleLabel.Text = scale + "x";

				foreach (var p in BattleManager.Participants)
				{
					p.AnimationScaler.SetScale(scale);
				}
			};

			speedPanel.AddChild(speed);

			Map.Position = PrimeGame.Center;

			Grid.Init(this);

			var dragger = new MonsterDragger();
			var spawner = new MonsterSpawner();

			var startBt = new Button("Start!", AnchorPoint.TopCenter, new Vector2(200, 50), new Vector2(0, 100));
			startBt.OnClick = () =>
			{
				ended = false;

				BattleManager.Sort();
				Level.Initialize();

				dragger.Destroy();
				spawner.Destroy();
				startBt.Unnatach();
			};

			var restartBt = new Button("Recomeçar", AnchorPoint.BottomRight, new Vector2(250, 50), new Vector2(0, 50));
			restartBt.OnClick = () =>
			{
				this.Game.ActiveScene = new LevelScene(parent, Level, false);
				this.Destroy();
			};

			var exitBt = new Button("Sair", AnchorPoint.BottomRight, new Vector2(250, 50));
			exitBt.OnClick = () =>
			{
				App.ResetSessions();

				this.Game.ActiveScene = parent;
				this.Destroy();
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

			// Build UI after the dialog is gone
			dialog.OnDone = () =>
			{
				Add(spawner);
				Add(dragger);
				AddUI(startBt);
				AddUI(restartBt);
				AddUI(exitBt);
				this.Add(new MousePositionHighlight(this.Map.TileWidth, this.Map.TileHeight));

				speedPanel.IsVisible = true;

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
			str = str.Replace("\t", "    ");

			if (console == null)
			{
				console = new Panel(new Vector2(400, 300), AnchorPoint.BottomLeft);
				console.Draggable = true;
				this.AddUI(console);
			}

			console.AddChild(new Label(str));
		}
	}
}
