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

		public static Sprite Character(string key)
		{
			return new Sprite(pics[key]);
		}
		
		public static void Initialize(ContentManager cm)
		{
			pics["ursula/normal"] = Overlord.Content.Sprites.Characters.Ursula.Normal(cm);
			pics["constanze/normal"] = Overlord.Content.Sprites.Characters.Constanze.Normal(cm);
		}
	}
}
