using Prime;
using Prime.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overlord
{
    public class Editor : Box
    {
		private const int width = 1280 / 2;
		private const int height = 500;

		private const int btWidth = width / 2;
		private const int btHeight = height / 6;

        private TextBox textBox;
		private Button saveButton;
		private Button closeButton;

        private Monster monster;
        public Monster Monster
        {
            get
            {
                return monster;
            }
            set
            {
                this.monster = value;
            }
        }

        private Editor(float w, float h, Sprite bg) : base(w, h, bg)
        { }

        public Editor(Monster m) : this(width, height, new RectangleSprite(width, height, Color.Green))
        {
            this.monster = m;
        }

        public override void Initialize()
        {
			base.Initialize();

            var font = this.Scene.Content.Load<SpriteFont>("Fonts/Editor");

            this.textBox = new TextBox(width, height - btHeight, font, new RectangleSprite(width, height - btHeight, Color.Black));
			this.textBox.Position = new Vector2(0, (textBox.Height - this.Height)/2 + 1);
			this.textBox.Text = monster.Lua.Content;
			this.textBox.ResetCarret();
			this.Insert(textBox);

			this.closeButton = new Button(btWidth, btHeight, new RectangleSprite(btWidth, btHeight, Color.Red), new RectangleSprite(btWidth, btHeight, Color.Green), "Close", font, () =>
			{
				this.Destroy();
			});
			closeButton.Position = new Vector2(width, height) / 2 - new Vector2(btWidth * 1.5f, btHeight / 2);
			this.Insert(closeButton);

			this.saveButton = new Button(btWidth, btHeight, new RectangleSprite(btWidth, btHeight, Color.Red), new RectangleSprite(btWidth, btHeight, Color.Green), "Save", font, () =>
			{
				System.Console.WriteLine(this.textBox.Text);
				monster.Lua.Content = this.textBox.Text;
			});
			saveButton.Position = new Vector2(width, height) / 2 - new Vector2(btWidth / 2, btHeight / 2);
			this.Insert(saveButton);
        }
    }
}
