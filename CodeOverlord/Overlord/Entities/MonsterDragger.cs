using Prime;

namespace Overlord
{
	public class MonsterDragger : Entity
	{
		private Combatant target;

		public override void Update()
		{
			if (Input.IsButtonDown(MouseButtons.Left))
			{
				if (target == null)
				{
					target = BattleManager.GetByPos(Grid.WorldToPoint(Input.MousePosition(this.Scene.Cam)));
					if (target != null && !target.Despawnable) target = null;
				}
				else
				{
					target.GridPos = Grid.WorldToPoint(Input.MousePosition(this.Scene.Cam));
				}
			}
			else
			{
				target = null;
			}
		}
	}
}
