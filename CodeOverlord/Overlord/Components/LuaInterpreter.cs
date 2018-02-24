using System;
using System.Linq;
using System.Collections.Generic;
using Prime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MoonSharp.Interpreter;

namespace Overlord
{
	public class LuaInterpreter : Component
	{
		public Script Script = new Script();

		public bool IsReady;

		private Coroutine routine;

		private string content = "";
		public string Content
		{
			get
			{
				return content;
			}
			set
			{
				this.Script = new Script();
				setupScript();
				this.Script.DoString(value);
				this.content = value;
			}
		}

		public LuaInterpreter()
		{
			LuaInterpreter.RegisterType<Point>();
			LuaInterpreter.RegisterType<Entity>();
			LuaInterpreter.RegisterType<Monster>();
			LuaInterpreter.RegisterType<Scene>();
		}

		public override void Initialize()
		{
			setupScript();
		}

		public override void Update()
		{
			if(routine == null && IsReady)
			{
				routine = Script.CreateCoroutine(Script.Globals["update"]).Coroutine;
				routine.AutoYieldCounter = 1;
				routine.Resume(1);
			}

			while(IsReady)
			{
				var result = routine.Resume();

				if(result.Type != DataType.YieldRequest)
				{
					routine = null;
					break;
				}
			}
		}

		private void setupScript()
		{
			Script.Globals["this"] = this.Owner;

			Script.Globals["monsters"] = (Func<List<Monster>>)(() => 
			{
				var list = new List<Monster>();
				
				foreach (var m in BattleManager.Monsters)
				{
					list.Add(m);
				}

				list.Remove((Monster) this.Owner);

				list = list.OrderBy((m) =>
				{
					var dist = ((Monster)Owner).Pos - m.Pos;
					return Math.Abs(dist.X) + Math.Abs(dist.Y);
				}).ToList();
				return list;
			});
		}

		private static List<Type> types = new List<Type>();
		public static void RegisterType<T>()
		{
			if(types.Contains(typeof(T)))
				return;

			types.Add(typeof(T));
			UserData.RegisterType<T>();
		}
	}
}
