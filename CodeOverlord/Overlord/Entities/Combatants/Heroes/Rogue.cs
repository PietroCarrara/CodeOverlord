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
			base.Initialize();

			base.Animations = Add(Content.Sprites.Heroes.Rogue.SpriteSheet(this.Scene));

			// Rogue Skills
			Lua.AddSkill(new SurpriseAttackUp());
			Lua.AddSkill(new SurpriseAttackDown());
			Lua.AddSkill(new SurpriseAttackLeft());
			Lua.AddSkill(new SurpriseAttackRight());
		}
	}
}
