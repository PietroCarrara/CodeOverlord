using Prime;

namespace Overlord
{
	public class MonsterSpawner : Entity
	{
		public string Script;

		public override void Update()
		{
			if(Input.IsButtonPressed(MouseButtons.Right))
			{
				base.Update();

				if (Script == "")
					return;

				var m = Monster.FromScript(Script);

				this.Scene.Add(m);

				m.GridPos = Grid.WorldToPoint(Input.MousePosition(this.Scene.Cam));

				BattleManager.Monsters.Add(m);
			}
		}
	}
}

