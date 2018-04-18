using Prime;
using Prime.Graphics;
using System.Linq;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using MonoGame.Extended.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

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

		public LevelScene(string scriptContent)
		{
			UserData.RegisterType<Point>();

			var script = new Script();
			var level = script.DoString(scriptContent).Table;

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
			Add(dragger);

			var spawner = new MonsterSpawner();
			Add(spawner);

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
			Add(bt);

			var selector = Add(new ScriptSelector(300, 300, spawner));
			selector.Position = new Vector2(1280 - selector.Width / 2, 720 / 2 - selector.Height / 2);

			// Camera setup
			camTarget = new Entity();
			camTarget.Position = Vector2.Zero;

			this.Cam.Add(new DelayedFollowTarget(camTarget, 10));
			this.Cam.Position = Vector2.Zero;
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
