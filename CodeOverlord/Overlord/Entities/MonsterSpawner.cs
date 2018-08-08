using Prime;

namespace Overlord
{
	public class MonsterSpawner : Entity
	{
		public VirtualFile Script;

		public override void Update()
		{
			// Spawn
			if(Input.IsButtonPressed(MouseButtons.Right))
			{
				base.Update();

				var p = Grid.WorldToPoint(Input.MousePosition(this.Scene.Cam));

				if (Script == null || !Grid.IsAvailable(p)) 
					return;

				var m = Monster.FromScript(Script.Text);
				m.Despawnable = true;
				this.Scene.Add(m);

				m.GridPos = p;

				BattleManager.Add(m);
			}

			// Despawn
			if (Input.IsButtonPressed(MouseButtons.Middle))
			{
				var p = Grid.WorldToPoint(Input.MousePosition(this.Scene.Cam));

				var m = BattleManager.GetByPos(p);

				if (m == null) return;

				if (m.Despawnable)
				{
					m.Destroy();
					BattleManager.Remove(m);
				}
			}
		}
	}
}

