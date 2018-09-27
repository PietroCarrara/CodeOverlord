using System;
using Prime;
using Prime.UI;
using Prime.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace CodeOverlord.Overlord.Scenes
{
	public class ConnectionLoadingScene : Scene
	{
		private bool done;

		const int sla = 40;

		public override void Initialize()
		{
			base.Initialize();

			this.ClearColor = Color.FromNonPremultiplied(40, 40, 40, 255);

			this.AddUI(new Label("Esperando editor de texto...", AnchorPoint.Center));

			var img = new SpriteSheet(Content.Load<Texture2D>("Sprites/UI/Animations/Loading"),
										new Point(78 * 2, 78 * 2),
										new Point(78, 78));

			img.Add("default", 0, 4, 1 / 8f);
			img.Play("default");

			img.Width = img.Height = 100;

			var e = new Entity();

			e.Add(img);

			this.Add(e);

			e.Position = new Vector2(1280 - img.Width / 2, 720 - img.Height / 2);
		}

		public override void Update()
		{
			base.Update();

			if (done)
			{
				this.Game.ActiveScene = new LevelSelectionScene();
				done = false;
				this.Destroy();
			}
		}

		public void Done()
		{
			this.done = true;
		}
	}
}
