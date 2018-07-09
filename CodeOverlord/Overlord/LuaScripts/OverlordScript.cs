using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using Prime;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class OverlordScript : Script
	{
		protected LevelScene Scene;

		public OverlordScript(LevelScene s)
		{
			this.Scene = s;

			this.Globals["monsters"] = (Func<List<ProxyCombatant>>)monsters;
			this.Globals["gridIsAvailable"] = (Func<int, int, bool>)gridIsAvailable;

			UserData.RegisterType<Point>();
			UserData.RegisterType<Vector2>();
			UserData.RegisterType<Entity>();
			UserData.RegisterType<Scene>();

			UserData.RegisterType<Hero>();

			UserData.RegisterType<Rogue>();
		}

		private List<ProxyCombatant> monsters()
		{
			var list = new List<ProxyCombatant>();

			foreach (var m in BattleManager.Monsters)
			{
				list.Add(new ProxyCombatant(m));
			}

			return list;
		}

		private List<ProxyCombatant> heroes()
		{
			var list = new List<ProxyCombatant>();

			foreach (var h in BattleManager.Heroes)
			{
				list.Add(new ProxyCombatant(h));
			}

			return list;
		}

		private bool gridIsAvailable(int x, int y)
		{
			return Grid.IsAvailable(x, y);
		}

	}
}
