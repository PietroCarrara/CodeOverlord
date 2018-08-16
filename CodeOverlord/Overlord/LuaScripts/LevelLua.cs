using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using Prime;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class LevelLua : OverlordScript
	{
		public LevelLua()
		{
			this.Globals["spawn"] = (Func<string, int, int, ProxyCombatant>)spawn;
			this.Globals["setScript"] = (Action<ProxyCombatant, string>)setScript;
		}

		private ProxyCombatant spawn(string name, int x = 0, int y = 0)
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
				default:
					// TODO: Custom exception
					throw new Exception("spawn: Invalid name: \"" + name + "\"");
			}

			c.GridPos = new Point(x, y);

			BattleManager.Add(c);
			PrimeGame.Game.ActiveScene.Add(c);
			return new ProxyCombatant(c);
		}

		private void setScript(ProxyCombatant combatant, string path)
		{
			var s = (LevelScene)combatant.Combatant.Scene;

			combatant.Combatant.SetScript(s.Level.Files[path]);
		}
	}
}

