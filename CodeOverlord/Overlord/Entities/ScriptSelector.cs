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
			var bg = this.Add(new RectangleSprite((int)this.Width, (int)this.Height, Color.Blue));
			bg.RelativePosition = new Vector2(0, this.Height) / 2;

			float btWidth = this.Width;
			float btHeight = this.Height / 10;

			btSprite = new RectangleSprite((int)btWidth, (int)btHeight, Color.Green);
			btHover = new RectangleSprite((int)btWidth, (int)btHeight);
			btFont = Scene.Content.Load<SpriteFont>("Fonts/Editor");

			var bt = new Prime.Button(btWidth, btHeight, btSprite, btHover, "Create New", btFont, () => 
			{
				this.Scene.Add(new ClassSelector(this));
			});
			bt.Position = new Vector2(0, this.Height - btHeight / 2);

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
