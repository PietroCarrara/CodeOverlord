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

		public bool CurrentInstructionDone = true;

		private Coroutine routine;

		public new Monster Owner;

		private string content = "";
		public string Content
		{
			get
			{
				return content;
			}
			set
			{
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
			this.Owner = (Monster) base.Owner;

			setupScript();
		}

		public void AddSkill(Skill s)
		{
			this.Script.Globals[s.Name] = s.GetCode();
		}

		public void DoTurn()
		{
			if (!CurrentInstructionDone)
				return;

			if(routine == null)
			{
				routine = Script.CreateCoroutine(Script.Globals["update"]).Coroutine;
				routine.AutoYieldCounter = 1;
				routine.Resume(1);
			}

			var result = routine.Resume();

			if(result.Type != DataType.YieldRequest)
			{
				routine = null;
			}

			if (Owner.CurrentStamina <= 0 && CurrentInstructionDone)
				BattleManager.Current = null;
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
					var dist = ((Monster)Owner).GridPos - m.GridPos;
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
