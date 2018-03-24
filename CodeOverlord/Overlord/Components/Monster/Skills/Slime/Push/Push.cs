using Prime;
using System;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class Push : Skill
	{
		private Point direction;

		public Push(Point p, string name) : base("push" + name)
		{
			this.direction = p;
		}

		public override object GetCode()
		{
			return (Action) run;
		}

		private void run()
		{
			Begin();

			this.Owner.CurrentStamina -= 1;

			var m = BattleManager.GetByPos(this.Owner.GridPos + direction);

			if (m == null)
			{
				End();
				return;
			}

			var targetPoint = m.GridPos + (m.GridPos - this.Owner.GridPos);

			// If the tile is not empty
			if (BattleManager.GetByPos(targetPoint) != null)
			{
				End();
				return;
			}

			var target = Grid.PointToWorld(m.GridPos + (m.GridPos - this.Owner.GridPos));
			var movement = target - m.Position;

			bool pushDone = false, animDone = false;

			Tasks.Create(
				() => m.Position += movement * Time.DetlaTime / 2,
				() => m.GridPos == targetPoint,
				() => pushDone = true
			);

			Owner.Animations.Play("attack", () =>
			{
				Owner.Animations.Play("idle");
				animDone = true;
			});

			Tasks.Create(
				() => {},
				() => animDone && pushDone,
				() => End()
			);
		}
	}
}
