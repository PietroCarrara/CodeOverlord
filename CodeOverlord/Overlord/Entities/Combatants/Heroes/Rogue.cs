using Prime;

namespace Overlord
{
	public class Rogue : Hero
	{
		public Rogue()
		{
			this.Stamina = 2;
			this.Damage = 1;
			this.Health = 2;
			this.Reach = 1;
		}

		public override void Initialize()
		{
			base.Animations = Add(Content.Sprites.Heroes.Rogue.SpriteSheet(this.Scene));

			base.Initialize();

			// Rogue Skills
			this.Add(new SurpriseAttackUp());
			this.Add(new SurpriseAttackDown());
			this.Add(new SurpriseAttackLeft());
			this.Add(new SurpriseAttackRight());
		}
	}
}
