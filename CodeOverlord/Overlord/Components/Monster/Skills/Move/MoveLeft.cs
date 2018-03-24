using System;
using Prime;
using MoonSharp.Interpreter;

namespace Overlord
{
	public class MoveLeft : Skill
	{
		private const float speed = 50;

		public MoveLeft() : base("moveLeft")
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
			gridPos.X--;

			var target = Grid.PointToWorld(gridPos);

			Owner.Animations.Play("walk");
			Owner.Animations.FlipX = false;

			Tasks.Create (
				() => Owner.Position.X -= speed * Time.DetlaTime,
				() => Owner.Position.X <= target.X,
				() => { Owner.CurrentStamina--; End(); this.Owner.Animations.Play("idle"); }
			);
		}
	}
}


