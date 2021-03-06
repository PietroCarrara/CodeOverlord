using Prime;
using Prime.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overlord.Content.Sprites.Heroes
{
	public static class Rogue 
	{
		public static SpriteSheet SpriteSheet(Scene s)
		{
			var tex = s.Content.Load<Texture2D>("Sprites/Heroes/Rogue/spritesheet");

			var anim = new SpriteSheet(tex, new Point(320), new Point(32));

            anim.Add("idle", 0, 10, 0.1f);
            anim.Add("walk", 20, 30, 0.1f);
            anim.Add("attack", 30, 40, 0.07f);
            anim.Add("die", 90, 100, 0.1f);

			anim.Play("idle");

            anim.Width = Grid.TileWidth;
            anim.Height = Grid.TileHeight;

			return anim;
		}
	}
}
