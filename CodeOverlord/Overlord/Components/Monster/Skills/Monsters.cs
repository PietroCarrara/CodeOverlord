using Prime;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Overlord
{
	public class Monsters : Skill
	{
		public Monsters() : base("monsters")
		{  }

		public override object GetCode()
		{
			return (Func<List<Monster>>) run;
		}

		private List<Monster> run()
		{
			var list = new List<Monster>();

			foreach (var m in BattleManager.Monsters)
			{
				list.Add(m);
			}

			// Don't include yourself in the list
			var monster = this.Owner as Monster;
			if (monster != null)
				list.Remove(monster);

			// Sort by distance
			list = list.OrderBy((m) =>
			{
				var dist = (this.Owner).GridPos - m.GridPos;
				return Math.Abs(dist.X) + Math.Abs(dist.Y);
			}).ToList();

			return list;
		}
	}
}
