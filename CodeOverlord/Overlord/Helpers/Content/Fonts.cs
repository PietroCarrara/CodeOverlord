using Prime;
using Microsoft.Xna.Framework.Graphics;

namespace Overlord.Content
{
	public static class Fonts
	{
		public static SpriteFont Editor(Scene s)
		{
			return s.Content.Load<SpriteFont>("Fonts/Editor");
		}
	}
}
