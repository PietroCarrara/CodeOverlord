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

		public ListView<string> Scripts;

		Sprite btSprite, btHover;
		SpriteFont btFont;

		public override void Initialize()
		{
			var bg = this.Add(new Sprite(Content.Sprites.UI.Panels.Panel0(this.Scene)));
			bg.Width = this.Width;
			bg.Height = this.Height;
			bg.RelativePosition = new Vector2(0, this.Height) / 2;

			float btWidth = this.Width - this.Width / 8;
			float btHeight = this.Height / 10;

			btSprite = new Sprite(Content.Sprites.UI.Buttons.Button0(this.Scene));
			btSprite.Width = btWidth;
			btSprite.Height = btHeight;

			btHover = new Sprite(Content.Sprites.UI.Buttons.Button0Hover(this.Scene));
			btHover.Width = btWidth;
			btHover.Height = btHeight;

			btFont = Content.Fonts.Editor(this.Scene);

			var bt = new Prime.Button(btWidth, btHeight, btSprite, btHover, "Create New", btFont, () => 
			{
				this.Scene.Add(new ClassSelector(this));
			});
			bt.Position = new Vector2(0, this.Height - btHeight / 2 - this.Height / 20);

			Scripts = new ListView<string>(Width, Height * .9f, 150, 150);
			Scripts.Position = new Vector2(0, Height * .9f / 2);
			Scripts.OnSelected = (s) =>
			{
				this.m.Script = s;
			};

			var scripts = ScriptIO.GetScripts();

			foreach ( var s in scripts )
			{
				var script = ScriptIO.Load(s);

				Scripts.Add(script, Content.Icons.Scroll(this.Scene));
			}

			this.Insert(Scripts);
			this.Insert(bt);
		}
	}
}
