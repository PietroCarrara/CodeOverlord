using Prime;
using System.Linq;
using System.Collections.Generic;

namespace Overlord
{
	public static class BattleManager
	{
		public static List<Monster> Monsters = new List<Monster>();

		// public static List<Hero> Heroes = new List<Hero>();

		public static Entity Current;
		private static int currIndex=-1;
		private static int total;

		private static List<Entity> participants = new List<Entity>();
		public static void Sort()
		{
			participants.Clear();
			participants.AddRange(Monsters);
			// participants.AddRange(Heroes);
			
			// Maybe have some kind of 'Speed' status?
			// By now just shuffle it
			participants.OrderBy((e) => Randomic.Rand(1000));

			total = participants.Count();
		}

		public static void Update()
		{
			if(Current == null)
			{
				currIndex++;
				if (currIndex >= total)
					currIndex = 0;

				Current = participants[currIndex];
				Current.GetComponent<LuaInterpreter>().IsReady = true;
			}
		}
	}
}
