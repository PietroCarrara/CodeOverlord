using Prime;
using Prime.Graphics;
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

		public string[] Heroes;
		private int spawnIndex = 0;

		public int Width, Height;

		public List<Point> Spawns = new List<Point>();

		public Dictionary<string, VirtualFile> VirtualFiles = new Dictionary<string, VirtualFile>();

		private Table level;

		public LevelScene(string scriptContent, string root)
		{
			UserData.RegisterType<Point>();

			var script = new Script();
			level = script.DoString(scriptContent).Table;

			this.Name = level.Get("name").String;
			this.Map = level.Get("map").String;

			this.Heroes = level.Get("heroes").Table.Values.AsObjects<string>().ToArray();

			this.Width = (int)level.Get("dimensions").Table.Get("x").Number;
			this.Height = (int)level.Get("dimensions").Table.Get("y").Number;

			foreach (var pos in level.Get("spawns").Table.Values)
			{
				var point = new Point();

				point.X = (int) pos.Table.Get("x").Number;
				point.Y = (int) pos.Table.Get("y").Number;
		
				this.Spawns.Add(point);
			}

			foreach (var s in level.Get("files").Table.Values.AsObjects<string>())
			{
				var content = ScriptIO.Load(root + s);

				this.VirtualFiles[s] = new VirtualFile(content);
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

			BattleManager.Init(this);

			var dragger = new MonsterDragger();

			var spawner = new MonsterSpawner();

			this.Add(new MousePositionHighlight(map.TileWidth, map.TileHeight));

			var font = Overlord.Content.Fonts.Editor(this);
			var btSpr = new Sprite(Overlord.Content.Sprites.UI.Buttons.Button0(this));
			var btSprHover = new Sprite(Overlord.Content.Sprites.UI.Buttons.Button0Hover(this));
			var bt = new Button(200, 50, btSpr, btSprHover, "Start!", font, null); 
			bt.OnClick = () =>
			{
				BattleManager.Sort();
				dragger.Destroy();
				spawner.Destroy();
				bt.Destroy();
			};
			bt.Position = PrimeGame.Center - new Vector2(0, 200);

			var selector = Add(new ScriptSelector(300, 300, spawner));
			
			foreach (var pair in this.VirtualFiles)
			{
				selector[pair.Key] = pair.Value;
			}

			selector.Position = new Vector2(1280 - selector.Width / 2, 720 / 2 - selector.Height / 2);
			selector.IsVisible = false;

			// Camera setup
			camTarget = new Entity();
			camTarget.Position = Vector2.Zero;

			this.Cam.Add(new DelayedFollowTarget(camTarget, 10));
			this.Cam.Position = Vector2.Zero;

			var dialogFont = Overlord.Content.Fonts.Dialogs(this.Content);
			var dialog = this.Add(new Dialog());
			foreach(var line in level.Get("dialog").Table.Values)
			{
				var character = line.Table.Get("char").String;
				var contents = line.Table.Get("contents").String;

				var l = this.Add(new Line(character, dialogFont, contents));

				dialog.Put(l);
			}

			// Build UI after the dialog is gone
			dialog.Add(new LineKeeper()).OnDone = () =>
			{
				Add(bt);
				Add(spawner);
				Add(dragger);
				selector.IsVisible = true;
			};

			dialog.Next();
		}

		public void OnEditorReady()
		{
			foreach (var pair in this.VirtualFiles)
			{
				App.CreateSession(pair.Key, pair.Value.Text);
			}
		}

		public void SetScriptText(string key, string text)
		{
			System.Console.WriteLine(text);
			this.VirtualFiles[key].Text = text;
		}

		public virtual void EndTurn()
		{
			foreach(var spawn in this.Spawns)
			{
				if (this.spawnIndex >= this.Heroes.Length)
					break;

				if (Grid.IsAvailable(spawn))
				{
					// Spawn
					var script = ScriptIO.Load(this.Heroes[spawnIndex]);
					var hero = Hero.FromScript(script);

					hero.GridPos = spawn;

					this.Add(hero);
					BattleManager.Add(hero);

					spawnIndex++;
				}
			}
		}

		public override void Update()
		{
			base.Update();

			BattleManager.Update();

			if (!Input.HasFocus())
			{
				return;
			}

			if (Input.MousePosition().X >= 1260 || Input.IsKeyDown(Keys.D))
				camTarget.Position.X += camSpeed;
			else if (Input.MousePosition().X <= 20 || Input.IsKeyDown(Keys.A))
				camTarget.Position.X -= camSpeed;

			if (Input.MousePosition().Y >= 700 || Input.IsKeyDown(Keys.S))
				camTarget.Position.Y += camSpeed;
			else if (Input.MousePosition().Y <= 20 || Input.IsKeyDown(Keys.W))
				camTarget.Position.Y -= camSpeed;
		}
	}
}
