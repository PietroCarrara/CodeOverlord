using System;
using Prime;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class Move : Skill
	{
		private Point movement;

		private const float speed = 50;

		public Move(string dir, Point movement) : base("move" + dir)
		{
			this.movement = movement;
		}

		public override object GetCode()
		{
			return (Action)run;
		}

		public void run()
		{
			Begin();

			var prevGridPos = this.Owner.GridPos;
			var gridPos = prevGridPos + movement;

			if (!Grid.IsAvailable(gridPos))
			{
				Owner.CurrentStamina--;
				End();
				return;
			}

			var target = Grid.PointToWorld(gridPos);

			Console.WriteLine(target);

			Owner.Animations.Play("walk");

			Tasks.Create(
				() => Owner.Position += speed * TimeControl.GetScale() * Time.DetlaTime * movement.ToVector2(),
				() => Math.Abs((Owner.Position - target).X) <= 1 && Math.Abs((Owner.Position - target).Y) <= 1 && prevGridPos != this.Owner.GridPos,
				() => { Owner.CurrentStamina--; End(); this.Owner.Animations.Play("idle"); }
			);
		}
	}
}
