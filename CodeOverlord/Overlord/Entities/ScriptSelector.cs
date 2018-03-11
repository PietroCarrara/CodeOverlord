using Prime;
using Prime.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overlord
{
	public class ScriptSelector : Box
	{
		private MonsterSpawner m;

		public ScriptSelector(float w, float h, MonsterSpawner m) : base(w, h)
		{
			this.m = m;
		}

		public ListView<Monster> Scripts;

		Sprite btSprite, btHover;
		SpriteFont btFont;

		public override void Initialize()
		{
			var bg = this.Add(new Sprite(this.Scene.Content.Load<Texture2D>("Sprites/UI/Panels/Panel0")));
			bg.Width = this.Width;
			bg.Height = this.Height;
			bg.RelativePosition = new Vector2(0, this.Height) / 2;

			float btWidth = this.Width - this.Width / 8;
			float btHeight = this.Height / 10;

			btSprite = new Sprite(this.Scene.Content.Load<Texture2D>("Sprites/UI/Buttons/Button0"));
			btSprite.Width = btWidth;
			btSprite.Height = btHeight;

			btHover = new Sprite(this.Scene.Content.Load<Texture2D>("Sprites/UI/Buttons/Button0Hover"));
			btHover.Width = btWidth;
			btHover.Height = btHeight;

			btFont = Scene.Content.Load<SpriteFont>("Fonts/Editor");

			var bt = new Prime.Button(btWidth, btHeight, btSprite, btHover, "Create New", btFont, () => 
			{
				this.Scene.Add(new ClassSelector(this));
			});
			bt.Position = new Vector2(0, this.Height - btHeight / 2 - this.Height / 20);

			Scripts = new ListView<Monster>(Width, Height * .9f, 150, 150);
			Scripts.Position = new Vector2(0, Height * .9f / 2);
			Scripts.OnSelected = (m) =>
			{
				this.m.Monster = m;
			};

			this.Insert(Scripts);
			this.Insert(bt);
		}
	}
}
