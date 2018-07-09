using Prime;
using System;
using System.Linq;
using System.Collections.Generic;
using MoonSharp.Interpreter;

namespace Overlord
{
	public class Heroes : Skill
	{
		public Heroes() : base("heroes")
		{ }

		public override object GetCode()
		{
			return (Func<List<ProxyCombatant>>)run;
		}

		private List<ProxyCombatant> run()
		{
			var list = new List<ProxyCombatant>();

			foreach (var h in BattleManager.Heroes)
			{
				list.Add(new ProxyCombatant(h));
			}

			var hero = this.Owner as Hero;
			if (hero != null)
				list.RemoveAll((x) => x.Combatant == hero);

			list = list.OrderBy((m) =>
			{
				var dist = this.Owner.GridPos - m.Combatant.GridPos;
				return Math.Abs(dist.X) + Math.Abs(dist.Y);
			}).ToList();

			return list;
		}
	}
}
