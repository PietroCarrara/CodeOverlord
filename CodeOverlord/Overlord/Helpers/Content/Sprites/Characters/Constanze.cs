using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Overlord.Content.Sprites.Characters
{
	public static class Constanze 
	{
		public static Texture2D Normal(ContentManager cm)
		{
			return cm.Load<Texture2D>("Sprites/Characters/Constanze/Normal");
		}
	}
}

