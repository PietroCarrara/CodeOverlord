using System;
using Prime;
using MoonSharp.Interpreter;

namespace Overlord
{
	public abstract class Skill : Component
	{
		// Name of the skill
		public readonly string Name;

		// Get the skill function callback
		public abstract object GetCode();

		public new Monster Owner;

		public Skill(string name)
		{
			this.Name = name;
		}

		protected void Begin()
		{
			Owner.Lua.CurrentInstructionDone = false;
		}

		protected void End()
		{
			Owner.Lua.CurrentInstructionDone = true;
		}

		public override void Initialize()
		{
			base.Initialize();

			this.Owner = (Monster) base.Owner;

			Owner.Lua.AddSkill(this);
		}
	}
}
