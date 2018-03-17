using Prime;
using System;

namespace Overlord
{
	public class AttackLeft : Skill
	{
		public AttackLeft() : base("attackLeft")
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

			Monster target = null;
			
			for (int i = 1; i <= Owner.CurrentReach; i++)
			{
				target = BattleManager.GetByPos(p.X - 1, p.Y);
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
