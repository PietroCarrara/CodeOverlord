using Prime;
using Prime.Graphics;
using Prime.UI;
using System.Linq;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using MonoGame.Extended.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Overlord.Editor;

namespace Overlord
{
	public class LevelScene : Scene
	{
		private Entity camTarget;
		private const float camSpeed = 15;

		public string Name;

		public string Map;

		public int Width, Height;

		public Dictionary<string, VirtualFile> VirtualFiles = new Dictionary<string, VirtualFile>();

		private Table level;

		private bool ended = true;

		private string path, root;

		public LevelScene(string path, string root)
		{
			this.path = path;
			this.root = root;

			BattleManager.Init(this);

			var scriptContent = ScriptIO.Load(path);

			var script = new LevelLua(this);
			level = script.DoString(scriptContent).Table;

			this.Name = level.Get("name").String;
			this.Map = level.Get("map").String;

			this.Width = (int)level.Get("dimensions").Table.Get("x").Number;
			this.Height = (int)level.Get("dimensions").Table.Get("y").Number;

			foreach (var s in level.Get("files").Table.Pairs)
			{
				var table = s.Value.Table;
				var filePath = table.Get("path").String;

				var content = ScriptIO.Load(root + filePath);

				var file = new VirtualFile(content);

				if (table.Get("readOnly").Boolean)
				{
					file.ReadOnly = true;
				}

				this.VirtualFiles[filePath] = file;
			}
		}

		public override void Initialize()
		{
			base.Initialize();

			this.ClearColor = Color.Gray;

			var map = this.Add(new TilingMap(Content.Load<TiledMap>(this.Map)));
			map.Width = this.Width;
			map.Height = this.Height;

			map.Position = PrimeGame.Center;

			Grid.Init(this);
			Grid.Width = map.WidthInTiles;
			Grid.Height = map.HeightInTiles;
			Grid.TileWidth = map.TileWidth;
			Grid.TileHeight = map.TileHeight;

			var dragger = new MonsterDragger();

			var spawner = new MonsterSpawner();

			var bt = new Button("Start!", Prime.UI.AnchorPoint.TopCenter, new Vector2(200, 50), new Vector2(0, 100));
			bt.OnClick = () =>
			{
				ended = false;

				BattleManager.Sort();
				dragger.Destroy();
				spawner.Destroy();
				bt.Destroy();
			};

			var selector = Add(new ScriptSelector(300, 300, spawner));
			
			foreach (var pair in this.VirtualFiles)
			{
				selector[pair.Key] = pair.Value;
			}

			selector.IsVisible = false;

			// Camera setup
			camTarget = new Entity();
			camTarget.Position = Vector2.Zero;

			this.Cam.Add(new DelayedFollowTarget(camTarget, 10));
			this.Cam.Position = Vector2.Zero;

			var dialog = this.Add(new Dialog());
			foreach(var line in level.Get("dialog").Table.Values)
			{
				var character = line.Table.Get("char").String;
				var contents = line.Table.Get("contents").String;

				var l = this.Add(new Line(character, contents));

				dialog.Put(l);
			}

			// Build UI after the dialog is gone
			dialog.Add(new LineKeeper()).OnDone = () =>
			{
				Add(spawner);
				Add(dragger);
				AddUI(bt);
				this.Add(new MousePositionHighlight(map.TileWidth, map.TileHeight));

				selector.IsVisible = true;
			};

			dialog.Next();
		}

		public void OnEditorReady()
		{
			foreach (var pair in this.VirtualFiles)
			{
				App.CreateSession(pair.Key, pair.Value.Text, pair.Value.ReadOnly);
			}
		}

		public void SetScriptText(string key, string text)
		{
			if (this.VirtualFiles.ContainsKey(key) && !this.VirtualFiles[key].ReadOnly)
				this.VirtualFiles[key].Text = text;
		}

		public virtual void UpdateLevel()
		{
			if (ended)
				return;

			var status = level.Get("update").Function.Call().String;
			switch(status)
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
				this.Game.Exit();
			};

			this.AddUI(bt);
		}

		private void lose()
		{
			ended = true;

			var bt = new Button("You've Lost! Go back...", AnchorPoint.Center, new Vector2(200, 50), new Vector2(0, -100)); 
			bt.OnClick = () =>
			{
				this.Game.Exit();
			};

			var bt2 = new Button("Retry", AnchorPoint.Center, new Vector2(200, 50), new Vector2(0, 100)); 
			bt2.OnClick = () =>
			{
				this.Game.ActiveScene = new LevelScene(path, root);
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
			// battle manager nor the level script
			if (ended)
				return;

			BattleManager.Update();

			this.UpdateLevel();
		}
	}
}
