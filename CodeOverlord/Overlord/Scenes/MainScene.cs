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

			var editor = Add(new EditorSpawner());

			var spawner = new MonsterSpawner();
			Add(spawner);

			var font = this.Content.Load<SpriteFont>("Fonts/Editor");
			var btSpr = new Sprite(this.Content.Load<Texture2D>("Sprites/UI/Buttons/Button0"));
			var btSprHover = new Sprite(this.Content.Load<Texture2D>("Sprites/UI/Buttons/Button0Hover"));
			var bt = new Button(200, 50, btSpr, btSprHover, "Start!", font, null); 
			bt.OnClick = () =>
			{
				BattleManager.Sort();
				dragger.Destroy();
				spawner.Destroy();
				editor.Destroy();
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
