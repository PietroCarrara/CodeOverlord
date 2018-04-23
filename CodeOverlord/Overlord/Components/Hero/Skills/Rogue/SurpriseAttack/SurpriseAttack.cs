using Prime;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class SurpriseAttack : Skill
	{
		private Point direction;

		private List<Monster> pastAttacks = new List<Monster>();

		public SurpriseAttack(Point p, string name) : base("surpriseAttack" + name)
		{
			this.direction = p;
		}

		public override object GetCode()
		{
			return (Action) run;
		}

		private void run()
		{
			var damage = Owner.CurrentDamage;

			Begin();

			this.Owner.CurrentStamina -= 1;

			var m = BattleManager.GetByPos(this.Owner.GridPos + direction) as Monster;

			if (m == null)
			{
				End();
				return;
			}

			if (pastAttacks.Contains(m))
			{
				damage = 0;
			}
			else
			{
				pastAttacks.Add(m);
			}

			Owner.Animations.Play("attack", () =>
			{
				Owner.Animations.Play("idle");
				m.ReceiveDamage(damage * 3, () =>
				{
					End();
					Owner.Animations.Play("idle");
				});
			});
		}
	}
}
