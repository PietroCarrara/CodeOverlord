using Prime;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public static class BattleManager
	{
		public static List<Monster> Monsters = new List<Monster>();

		public static List<Hero> Heroes = new List<Hero>();

		private static LevelScene scene;

		public static Combatant Current;
		private static int currIndex=-1;
		private static int total;

		private static List<Combatant> participants = new List<Combatant>();
		public static List<Combatant> Participants
		{
			get
			{
				return participants;
			}
		}

		public static void Init(LevelScene s)
		{
			scene = s;
		}

		public static void Sort()
		{
			participants.Clear();
			participants.AddRange(Monsters);
			participants.AddRange(Heroes);
			
			// Maybe have some kind of 'Speed' status?
			// By now just shuffle it
			participants = participants.OrderBy((e) => Randomic.Rand()).ToList();

			total = participants.Count();
		}

		public static void Add(Hero h)
		{
			Heroes.Add(h);
			// TODO: Add in order (higher iniciative comes first, that sort of thing)
			participants.Add(h);

			total++;
		}

		public static void Add(Monster m)
		{
			Monsters.Add(m);
			// TODO: Add in order (higher iniciative comes first, that sort of thing)
			participants.Add(m);

			total++;
		}

		public static void Remove(Monster m)
		{
			Monsters.Remove(m);
			participants.Remove(m);
			
			total--;
		}

		public static void Remove(Hero m)
		{
			Heroes.Remove(m);
			participants.Remove(m);
			
			total--;
		}

		public static void Update()
		{
			if (total <= 0)
				return;

			if(Current == null)
			{
				currIndex++;
				
				// A turn has passed, reset the order
				if (currIndex >= total)
				{
					currIndex = 0;
					scene.EndTurn();
				}

				Current = participants[currIndex];
				Current.ReceiveTurn();
			}
			
			Current.GetComponent<LuaInterpreter>().DoTurn();
		}

		public static bool IsEmpty(int x, int y)
		{
			return IsEmpty(new Point(x, y));
		}

		public static bool IsEmpty(Point p)
		{
			return GetByPos(p) == null;
		}

		public static Combatant GetByPos(int x, int y)
		{
			var p = new Point(x, y);

			foreach(var m in participants)
			{
				if(m.GridPos == p)
				{
					return m;
				}
			}

			return null;
		}

		public static Combatant GetByPos(Point p)
		{
			return GetByPos(p.X, p.Y);
		}
	}
}
