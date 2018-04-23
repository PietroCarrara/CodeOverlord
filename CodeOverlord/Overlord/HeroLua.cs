using MoonSharp.Interpreter;
using System;
using Prime;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class HeroLua : LuaInterpreter
	{
		public HeroLua()
		{
			base.Script.Globals["Rogue"] = (Func<Table>)(() =>
			{
				var t = combatant();
				t["base"] = new Rogue();
				return t;
			});

			UserData.RegisterType<Point>();
			UserData.RegisterType<Vector2>();
			UserData.RegisterType<Entity>();
			UserData.RegisterType<Scene>();

			UserData.RegisterType<Hero>();

			UserData.RegisterType<Rogue>();
		}
	}
}
