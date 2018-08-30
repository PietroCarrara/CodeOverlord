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

		public static Combatant Current;
		private static int currIndex = -1;
		private static int total;

		private static LevelScene scene;

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
			Monsters = new List<Monster>();
			Heroes = new List<Hero>();
			participants = new List<Combatant>();

			scene = s;

			currIndex = -1;
			total = 0;

			Current = null;
		}

		public static void Sort()
		{
			participants.Clear();
			participants.AddRange(Monsters);
			participants.AddRange(Heroes);

			// Maybe have some kind of 'Speed' status?
			// By now just shuffle it
			// participants = participants.OrderBy((e) => Randomic.Rand()).ToList();

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

		public static void Add(Combatant c)
		{
			if (c is Monster m)
			{
				Add(m);
			}
			else if (c is Hero h)
			{
				Add(h);
			}
			else
			{
				// TODO: Throw custom exception
				throw new System.Exception("Combatant is not a Hero nor a Monster!");
			}
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

		public static void Remove(Combatant c)
		{
			if (c is Monster m)
			{
				Remove(m);
			}
			else if (c is Hero h)
			{
				Remove(h);
			}
			else
			{
				// TODO: Throw custom exception
				throw new System.Exception("Combatant is not a Hero nor a Monster!");
			}
		}


		public static void Update()
		{
			if (total <= 0)
			{
				scene.CheckWin();
				return;
			}

			if (Current == null)
			{
				currIndex++;

				scene.CheckWin();

				// A turn has passed, reset the order
				if (currIndex >= total)
				{
					currIndex = 0;
				}

				Current = participants[currIndex];
				Current.ReceiveTurn();
			}

			Current.Lua.DoTurn();
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

			foreach (var m in participants)
			{
				if (m.GridPos == p)
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
