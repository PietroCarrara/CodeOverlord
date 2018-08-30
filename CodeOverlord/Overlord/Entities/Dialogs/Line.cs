using System;
using System.Linq;
using Prime;
using Prime.UI;
using Prime.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Overlord
{
	public class Line : Entity
	{
		// Text to be displayed
		public string Contents;

		// Textbox to display
		private Panel panel;
		private Label label;

		// The picture talking
		public Image Character;
		public string Name { get; private set; }

		private SoundEffect effect;

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
		private Point boxSize = new Point(1280, 150);

		// Chars that should pause the speech a bit
		private char[] pauseChars = new char[] { '!', ',', '?', '.', ':' };

		// How long to wait to print the next char in seconds.
		// Defaults as 8 chars per second
		private const float charsDelay = 1f / 30f;

		// Start couting when we appear
		public bool IsVisible
		{
			get
			{
				return panel.IsVisible;
			}
			set
			{
				if (value)
				{
					this.current = this.beginning = Time.TotalGameTime;
					this.elapsed = new TimeSpan();
				}
				panel.IsVisible = value;
				Character.IsVisible = value;
			}
		}

		public Line(string character, string contents)
		{
			this.Contents = contents;

			this.Character = new Image(CharManager.Character(character), AnchorPoint.BottomLeft);

			this.Name = character;
		}

		public void Reset()
		{
			base.Initialize();

			this.totalChars = 0;

			this.effect = this.Scene.Content.Load<SoundEffect>("Sounds/Slap");

			this.Scene.AddUI(this.Character);

			this.label = new Label("");

			this.panel = this.Scene.AddUI(new Panel(new Vector2(0, boxSize.Y), AnchorPoint.BottomCenter));
			this.panel.AddChild(this.label);

			// ________
			// |  :)  |
			// --------
			//   Hello, traveler
			if (Character.Size.X >= Character.Size.Y)
			{
				Character.Offset = new Vector2(0, boxSize.Y);
			}
			// ______
			// |    |
			// | :) |
			// | Hello, traveler
			// ------
			else
			{
				Character.Offset = new Vector2(0, 0);
			}
		}

		public void Attach()
		{
			this.Character.Attach();
		}

		public void Unatatch()
		{
			this.Character.Unnatach();
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
					label.Text = Contents.Substring(0, totalChars);

					if (pauseChars.Contains(Contents[totalChars - 1]))
					{
						// Wait 4 times more
						beginning += TimeSpan.FromSeconds(4 * charsDelay);
					}
					else if (totalChars % 3 == 0)
					{
						effect.Play(0.3f, 0, 0);
					}
				}
			}

			// Double clicking to fully draw line
			if (this.IsVisible && totalChars >= 3 && Input.IsButtonPressed(MouseButtons.Left))
			{
				label.Text = this.Contents;
				totalChars = Contents.Length;
			}
		}
	}
}
