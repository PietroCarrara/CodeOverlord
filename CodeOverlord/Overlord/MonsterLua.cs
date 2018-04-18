using Prime;
using Microsoft.Xna.Framework;
using MoonSharp.Interpreter;
using System;

namespace Overlord
{
	public class MonsterLua : LuaInterpreter
	{
		public MonsterLua()
		{
			base.Script.Globals["Slime"] = (Func<Slime>)(() => new Slime());

			UserData.RegisterType<Point>();
			UserData.RegisterType<Vector2>();
			UserData.RegisterType<Entity>();
			UserData.RegisterType<Scene>();
			UserData.RegisterType<Monster>();
			UserData.RegisterType<Slime>();
		}
	}
}
