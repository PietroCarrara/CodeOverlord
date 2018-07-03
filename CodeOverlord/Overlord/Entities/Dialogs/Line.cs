using System;
using System.Linq;
using Prime;
using Prime.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overlord
{
	public class Line : UIEntity 
	{
		// Text to be displayed
		public string Contents;

		// Textbox to display
		private TextBox text;

		// The picture talking
		public Sprite Character;
		public string Name { get; private set; }

		// The text font
		private SpriteFont font;

		// Used to check time on drawing characters
		private TimeSpan beginning;
		private TimeSpan elapsed;
		private TimeSpan current;

		// Counter of currently displaying chars
		private int totalChars;

		// Have we finished writing text?
		public bool IsFinished
		{
			get
			{
				return totalChars >= Contents.Length;
			}
		}

		// Sizings
		private Point boxSize = new Point(1280, 200);
		private Point margin = new Point(40, 30);

		// Chars that should pause the speech a bit
		private char[] pauseChars = new char[]{'!', ',', '?', '.'};

		// How long to wait to print the next char in seconds.
		// Defaults as 8 chars per second
		private const float charsDelay = 1f / 30f;

		// Start couting when we appear
		public override bool IsVisible
		{
			set
			{
				if (value)
				{
					this.current = this.beginning = Time.TotalGameTime;
					this.elapsed = new TimeSpan();
				}
				base.IsVisible = value;
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
			
			text.Position = new Vector2(1280 / 2f, 720 - boxSize.Y / 2f);

			this.Insert(text);

			this.Add(Character);

			// ________
			// |  :)  |
			// --------
			//   Hello, traveler
			if (Character.Width >= Character.Height)
			{
				Character.RelativePosition = new Vector2(Character.Width / 2f, 720 - boxSize.Y - Character.Height / 2f);
			}
			// ______
			// |    |
			// | :) |
			// | Hello, traveler
			// ------
			else
			{
				Character.RelativePosition = new Vector2(Character.Width / 2f, 720 - Character.Height / 2f);
			}
		}



		public override void Update()
		{
			base.Update();

			if (this.IsVisible && !this.IsFinished)
			{
				current += Time.DeltaGameTime;
				elapsed = current - beginning;

				if (elapsed.TotalSeconds >= charsDelay)
				{
					beginning = current;

					totalChars++;
					text.Text = Contents.Substring(0, totalChars);

					if (pauseChars.Contains(Contents[totalChars - 1]))
					{
						// Wait 4 times more
						beginning += TimeSpan.FromSeconds(10 * charsDelay);
					}
				}
			}

			// Double clicking to fully draw line
			if (this.IsVisible && totalChars >= 4 && Input.IsButtonPressed(MouseButtons.Left))
			{
				text.Text = this.Contents;
				totalChars = Contents.Length;
			}
		}
	}
}
