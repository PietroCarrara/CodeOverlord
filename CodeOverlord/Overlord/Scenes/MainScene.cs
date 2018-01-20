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
			Grid.TileWidth = 64;
			Grid.TileHeight = 64;

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
