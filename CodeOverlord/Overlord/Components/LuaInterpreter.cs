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
				this.content = value;
			}
		}

		public override void Initialize()
		{
			base.Initialize();

			this.Owner = (Combatant) base.Owner;
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
				routine = Script.CreateCoroutine(This["update"]).Coroutine;
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
	}
}
