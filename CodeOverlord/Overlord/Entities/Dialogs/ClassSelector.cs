using Prime;
using Prime.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overlord
{
	public class ClassSelector : UIEntity
	{
		private const int width = 480, height = 160;

		private const int borderW = 48, borderH = 16;

		private ScriptSelector scripts;
		
		private ListView<string> classes;

		public ClassSelector(ScriptSelector s) : base(width + borderW, height + borderH)
		{  
			this.scripts = s;
		}

		public override void Initialize()
		{
			// Background
			var bg = this.Add(new Sprite(Content.Sprites.UI.Panels.Panel0(this.Scene)));
			bg.Width = this.Width + borderW;
			bg.Height = this.Height + borderH;

			this.Position = PrimeGame.Center;

			var btBg = new RectangleSprite(width, height / 10);

			var font = Content.Fonts.Editor(this.Scene);

			var done = new Button(width, height / 10, btBg, btBg, "Done", font, close);
			done.Position = new Vector2(0, height / 2 - height / 10 / 2 + borderH / 2);

			classes = new ListView<string>(width, height * 0.9f, width / 3f + 30, height * 0.9f);
			classes.Position = new Vector2(0, -height / 2 + height * 0.9f / 2);

			this.Insert(done);
			this.Insert(classes);

			// Add all monsters
			classes.Add(Slime.DefaultCode, Content.Sprites.Monsters.Slime.SpriteSheet(this.Scene));
		}

		private void close()
		{
			if (classes.Selected != "")
			{
				var editor = this.Scene.Add(new Editor(classes.Selected));
				editor.Position = PrimeGame.Center;

				
				editor.OnSave = (src) =>
				{
					scripts.Scripts.Add(src, Content.Icons.Scroll(this.Scene));
					
					editor.Destroy();
				};
			}

			this.Destroy();
		}
	}
}
