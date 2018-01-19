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
			Add(s);

			var s2 = new Slime();
			s2.Pos = new Point(20, 20);
			Add(s2);

			var font = this.Content.Load<SpriteFont>("Fonts/Arial");
			var bt = new Button(200, 50, new RectangleSprite(200, 50, Color.Green), new RectangleSprite(200, 50, Color.Red), "Start!", font, () =>
			{
				BattleManager.Sort();
			});
			Add(bt);
		}

		public override void Update()
		{
			base.Update();

			BattleManager.Update();
		}
	}
}
