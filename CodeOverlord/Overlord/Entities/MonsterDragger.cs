using Prime;

namespace Overlord
{
	public class MonsterDragger : Entity
	{
		private Monster target;

		public override void Update()
		{
			if(Input.IsButtonDown(MouseButtons.Left))
			{
				if(target == null)
				{
					target = BattleManager.GetByPos(Grid.WorldToPoint(Input.MousePosition(this.Scene.Cam)));
				}
				else
				{
					target.Pos = Grid.WorldToPoint(Input.MousePosition(this.Scene.Cam));
				}
			}
			else
			{
				target = null;
			}
		}
	}
}
