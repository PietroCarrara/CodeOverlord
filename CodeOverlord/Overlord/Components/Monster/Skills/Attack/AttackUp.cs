using Prime;
using System;

namespace Overlord
{
	public class AttackUp : Skill
	{
		public AttackUp() : base("attackUp")
		{  }

		public override object GetCode()
		{
			return (Action) run;
		}

		private void run()
		{
			Begin();

			Owner.CurrentStamina--;

			var p = Owner.GridPos;

			Combatant target = null;
			
			for (int i = 1; i <= Owner.CurrentReach; i++)
			{
				target = BattleManager.GetByPos(p.X, p.Y-i);
			}

			if (target == null)
			{
				End();
				return;
			}

			Owner.Animations.Play("attack", () =>
			{
				Owner.Animations.Play("idle");
				target.ReceiveDamage(Owner.CurrentDamage, () =>
				{
					End();
					Owner.Animations.Play("idle");
				});
			});
		}
	}
}
