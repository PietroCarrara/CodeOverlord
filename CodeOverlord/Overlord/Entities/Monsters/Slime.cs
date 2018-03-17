using Prime;
using Prime.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace Overlord
{
	public class Slime : Monster
	{
		public Slime()
		{
			this.Stamina = 1;
			this.Damage = 1;
			this.Health = 2;
			this.Reach = 1;
		}

		public override void Initialize()
		{
			base.Initialize();

			var tex = this.Scene.Content.Load<Texture2D>("Sprites/Monsters/Slime/spritesheet");

			Animations = new SpriteSheet(tex, new Point(320, 640), new Point(32));

            Animations.Add("idle", 0, 10, 0.1f);
            Animations.Add("walk", 20, 30, 0.1f);
            Animations.Add("attack", 30, 40, 0.07f);
            Animations.Add("die", 40, 50, 0.1f);

			Animations.Play("idle");

            Animations.Width = Grid.TileWidth;
            Animations.Height = Grid.TileHeight;

			Add(Animations);

			LuaInterpreter.RegisterType<Slime>();
		}
	}
}
