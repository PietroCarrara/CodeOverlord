using Prime;
using System;

namespace Overlord
{
	public class PassTurn : Skill
	{
		public PassTurn() : base("passTurn")
		{  }

		public override object GetCode()
		{
			return (Action) run;
		}

		private void run()
		{

			this.Owner.CurrentStamina = 0;

			Begin();
			End();
		}
	}
}
