using Prime;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Overlord.Content
{
	public static class Fonts
	{
		public static SpriteFont Editor(Scene s)
		{
			return s.Content.Load<SpriteFont>("Fonts/Editor");
		}

		public static SpriteFont Dialogs(ContentManager cm)
		{
			return cm.Load<SpriteFont>("Fonts/Dialogs");
		}
	}
}
