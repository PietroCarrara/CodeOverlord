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
			base.Script.Globals["Rogue"] = (Func<Rogue>)(() => new Rogue());

			UserData.RegisterType<Point>();
			UserData.RegisterType<Vector2>();
			UserData.RegisterType<Entity>();
			UserData.RegisterType<Scene>();

			UserData.RegisterType<Hero>();

			UserData.RegisterType<Rogue>();
		}
	}
}
