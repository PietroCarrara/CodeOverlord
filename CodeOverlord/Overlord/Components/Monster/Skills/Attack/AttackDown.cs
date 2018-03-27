using Prime;
using System;

namespace Overlord
{
	public class AttackDown : Skill
	{
		public AttackDown() : base("attackDown")
		{  }

		public override object GetCode()
		{
			return (Action) run;
		}

		private void run()
		{
			Begin();

			Console.WriteLine("Attacking down!");

			Owner.CurrentStamina--;

			var p = Owner.GridPos;

			Combatant target = null;
			
			for (int i = 1; i <= Owner.CurrentReach; i++)
			{
				target = BattleManager.GetByPos(p.X, p.Y+i);
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
