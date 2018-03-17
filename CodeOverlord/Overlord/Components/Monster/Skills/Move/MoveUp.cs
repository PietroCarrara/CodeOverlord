using System;
using Prime;
using MoonSharp.Interpreter;

namespace Overlord
{
	public class MoveUp : Skill
	{
		private const float speed = 50;

		public MoveUp() : base("moveUp")
		{

		}

		public override object GetCode()
		{
			return (Action)run;
		}

		private void run()
		{
			Begin();

			var gridPos = this.Owner.GridPos;
			gridPos.Y--;

			var target = Grid.PointToWorld(gridPos);

			Owner.Animations.Play("walk");

			Tasks.Create (
				() => Owner.Position.Y -= speed * Time.DetlaTime,
				() => Owner.Position.Y <= target.Y,
				() => { Owner.CurrentStamina--; End(); this.Owner.Animations.Play("idle"); }
			);
		}
	}
}
