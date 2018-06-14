using Prime;
using Prime.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overlord
{
	public class Line : UIEntity 
	{
		public string Contents;

		private TextBox text;

		public Sprite Character;
		public string Name { get; private set; }

		private SpriteFont font;

		private Point boxSize = new Point(1280, 200);
		private Point margin = new Point(40, 30);

		public bool IsVisible
		{
			set
			{
				text.IsVisible = Character.IsVisible = value;
			}
		}

		public Line(string character, SpriteFont font, string contents)
		{
			this.Position = Vector2.Zero;

			this.Contents = contents;

			this.font = font;

			this.Character = CharManager.Character(character);
			this.Name = character;
		}

		public override void Initialize()
		{
			base.Initialize();

			this.text = new TextBox(boxSize.X - margin.X, boxSize.Y - margin.Y, font, new RectangleSprite(boxSize.X, boxSize.Y, Color.FromNonPremultiplied(0, 0, 0, 200)));
			
			text.Text = this.Contents;

			text.Position = new Vector2(1280 / 2f, 720 - boxSize.Y / 2f);

			this.Insert(text);

			this.Add(Character);

			// ________
			// |  :)  |
			// --------
			if (Character.Width >= Character.Height)
			{
				Character.RelativePosition = new Vector2(Character.Width / 2f, 720 - boxSize.Y - Character.Height / 2f);
			}
			// ______
			// |    |
			// | :) |
			// |    |
			// ------
			else
			{
				Character.RelativePosition = new Vector2(Character.Width / 2f, 720 - Character.Height / 2f);
			}
		}
	}
}
