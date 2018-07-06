using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using Prime;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class LevelLua : Script
	{
		private LevelScene scene;

		public LevelLua(LevelScene s)
		{
			this.scene = s;

			this.Globals["monsters"] = (Func<List<ProxyCombatant>>) monsters;
			this.Globals["spawn"] = (Func<string, int, int, Combatant>)spawn;

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

		private Combatant spawn(string name, int x = 0, int y = 0)
		{
			Combatant c = null;

			switch (name.ToLower())
			{
				case "slime":
					c = new Slime();
					break;
				case "rogue":
					c = new Rogue();
					break;
			}

			c.GridPos = new Point(x, y);

			BattleManager.Add(c);
			this.scene.Add(c);
			return c;
		}
	}
}

