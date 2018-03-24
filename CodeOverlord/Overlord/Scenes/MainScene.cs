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

			this.ClearColor = Color.Black;

			Grid.Width = 20;
			Grid.Height = 20;
			Grid.TileWidth = 64;
			Grid.TileHeight = 64;

			var dragger = new MonsterDragger();
			Add(dragger);

			var spawner = new MonsterSpawner();
			Add(spawner);

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
		}

		public override void Update()
		{
			base.Update();

			BattleManager.Update();
		}
	}
}
