using Prime;
using Prime.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overlord
{
	public class ClassSelector : Box
	{
		private const int width = 480, height = 160;

		private const int borderW = 48, borderH = 16;

		private ScriptSelector scripts;
		
		private List<Monster> monsters = new List<Monster>{ new Slime(), new Slime(), new Slime() };

		private ListView<Monster> classes;

		public ClassSelector(ScriptSelector s) : base(width + borderW, height + borderH)
		{  
			this.scripts = s;
		}

		public override void Initialize()
		{
			// Background
			var bg = this.Add(new Sprite(this.Scene.Content.Load<Texture2D>("Sprites/UI/Panels/Panel0")));
			bg.Width = this.Width + borderW;
			bg.Height = this.Height + borderH;

			this.Position = PrimeGame.Center;

			var btBg = new RectangleSprite(width, height / 10);

			var font = Scene.Content.Load<SpriteFont>("Fonts/Editor");

			var done = new Button(width, height / 10, btBg, btBg, "Done", font, close);
			done.Position = new Vector2(0, height / 2 - height / 10 / 2 + borderH / 2);

			classes = new ListView<Monster>(width, height * 0.9f, width / 3f + 30, height * 0.9f);
			classes.Position = new Vector2(0, -height / 2 + height * 0.9f / 2);

			this.Insert(done);
			this.Insert(classes);

			foreach (var m in monsters)
			{
				this.Scene.Add(m);
				classes.Add(m, m.GetComponent<Sprite>());
				m.GetComponent<Sprite>().IsVisible = false;
			}
		}

		private void close()
		{
			if (classes.Selected != null)
			{
				var editor = this.Scene.Add(new Editor(500, 500));
				editor.Position = PrimeGame.Center;
				
				editor.OnSave = (src) =>
				{
					classes.Selected.Lua.Content = src;

					classes.Selected.GetComponent<Sprite>().IsVisible = true;

					scripts.Scripts.Add(classes.Selected, this.Scene.Content.Load<Texture2D>("Icons/Scroll"));
					
					classes.Selected.GetComponent<Sprite>().IsVisible = false;

					editor.Destroy();
				};
			}

			foreach(var m in monsters)
			{
				if (m != classes.Selected)
					m.Destroy();
			}

			this.Destroy();
		}
	}
}
