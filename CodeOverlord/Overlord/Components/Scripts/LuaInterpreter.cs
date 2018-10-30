using System;
using System.Linq;
using System.Collections.Generic;
using Prime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MoonSharp.Interpreter;
using Overlord.Editor;
using CodeOverlord.Overlord.Systems;
using System.Diagnostics;

namespace Overlord
{
	public class LuaInterpreter : Component
	{
		public Script Script = new OverlordScript();
		public MoonSharp.Interpreter.Table This;

		public bool CurrentInstructionDone = true;

		private Coroutine routine;

		public new Combatant Owner;

		private string content = "";
		public string Content
		{
			get
			{
				return content;
			}
			set
			{
				This = this.Script.DoString(value).Table;

				if (This["update"] == null)
				{
					This["update"] = Script.DoString("return function() passTurn() end");
				}

				this.content = value;
			}
		}

		public override void Initialize()
		{
			base.Initialize();

			this.Owner = (Combatant)base.Owner;
		}

		public void AddSkill(Skill s)
		{
			this.Script.Globals[s.Name] = s.GetCode();
		}

		public void DoTurn()
		{
			if (!CurrentInstructionDone)
				return;

			try
			{
				if (routine == null)
				{
					routine = Script.CreateCoroutine(This["update"]).Coroutine;
					routine.AutoYieldCounter = 1;
					routine.Resume(1);
				}

				var result = routine.Resume();

				if (result.Type != DataType.YieldRequest)
				{
					routine = null;
					this.Owner.timesRun++;
					BattleManager.Current = null;
				}
			}
			catch (InterpreterException e)
			{
				LuaErrorHandler.Handle(e);

				if (this.Owner.Scene is LevelScene level)
				{
					level.Lose();
				}

				routine = null;
			}


			if (Owner.CurrentStamina <= 0 && CurrentInstructionDone)
				BattleManager.Current = null;
		}

		protected virtual Table combatant()
		{
			var t = new Table(this.Script);

			t["getPosition"] = (Func<Point>)(() => this.Owner.GridPos);

			return t;
		}
	}
}
