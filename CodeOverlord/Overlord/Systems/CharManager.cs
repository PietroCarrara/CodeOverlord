using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Prime;
using Prime.Graphics;

namespace Overlord
{
	public static class CharManager
	{
		private static Dictionary<string, Texture2D> pics = new Dictionary<string, Texture2D>();

		public static Texture2D Character(string key)
		{
			return pics[key];
		}
		
		public static void Initialize(ContentManager cm)
		{
			pics["constanze/normal"] = Overlord.Content.Sprites.Characters.Constanze.Normal(cm);
			pics["constanze/scared"] = Overlord.Content.Sprites.Characters.Constanze.Scared(cm);

			pics["pepper/normal"] = Overlord.Content.Sprites.Characters.Pepper.Normal(cm);
			pics["pepper/scared"] = Overlord.Content.Sprites.Characters.Constanze.Normal(cm);
		}
	}
}
