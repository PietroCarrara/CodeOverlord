using Prime;
using Prime.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overlord
{
	public class MainScene : Scene
	{
		public override void Initialize()
		{
			base.Initialize();

			Grid.Width = 20;
			Grid.Height = 20;
			Grid.TileWidth = 16;
			Grid.TileHeight = 16;

			var s = new Slime();
			s.Pos = new Point(1, 1);
			Add(s);

			var s2 = new Slime();
			s2.Pos = new Point(20, 20);
			Add(s2);

			var dragger = new MonsterDragger();
			Add(dragger);

			var spawner = new MonsterSpawner();
			Add(spawner);

			var font = this.Content.Load<SpriteFont>("Fonts/Arial");
			var bt = new Button(200, 50, new RectangleSprite(200, 50, Color.Green), new RectangleSprite(200, 50, Color.Red), "Start!", font, null); 
			bt.OnClick = () =>
			{
				BattleManager.Sort();
				dragger.Destroy();
				spawner.Destroy();
				bt.Destroy();
			};
			Add(bt);
		}

		public override void Update()
		{
			base.Update();

			BattleManager.Update();
		}
	}
}
