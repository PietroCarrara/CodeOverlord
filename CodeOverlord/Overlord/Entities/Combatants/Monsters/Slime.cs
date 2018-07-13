using Prime;
using Prime.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

			base.Animations = Add(Content.Sprites.Monsters.Slime.SpriteSheet(this.Scene));

			// Slime skills
			this.Add(new PushUp());
			this.Add(new PushDown());
			this.Add(new PushLeft());
			this.Add(new PushRight());
		}
	}
}
