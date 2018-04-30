using Prime;
using System;
using System.Linq;
using System.Collections.Generic;
using MoonSharp.Interpreter;

namespace Overlord
{
	public class Monsters : Skill
	{
		public Monsters() : base("monsters")
		{  }

		public override object GetCode()
		{
			return (Func<List<ProxyCombatant>>) run;
		}

		private List<ProxyCombatant> run()
		{
			var list = new List<ProxyCombatant>();

			foreach (var m in BattleManager.Monsters)
			{
				list.Add(new ProxyCombatant(this.Owner.Lua.Script, m));
			}

			var monster = this.Owner as Monster;
			if (monster != null)
				list.RemoveAll((x) => x.Combatant == monster);

			list = list.OrderBy((m) =>
			{
				var dist = this.Owner.GridPos - m.Combatant.GridPos;
				return Math.Abs(dist.X) + Math.Abs(dist.Y);
			}).ToList();

			return list;
		}
	}
}
