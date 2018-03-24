using Prime;
using Microsoft.Xna.Framework.Graphics;

namespace Overlord.Content.Sprites.UI
{
	public static class Buttons 
	{
		public static Texture2D Button0(Scene s)
		{
			return s.Content.Load<Texture2D>("Sprites/UI/Buttons/Button0");
		}
		
		public static Texture2D Button0Hover(Scene s)
		{
			return s.Content.Load<Texture2D>("Sprites/UI/Buttons/Button0Hover");
		}
	}
}
