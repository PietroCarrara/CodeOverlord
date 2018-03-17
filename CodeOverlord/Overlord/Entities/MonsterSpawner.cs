using Prime;

namespace Overlord
{
	public class MonsterSpawner : Entity
	{
		public Monster Monster;

		public override void Update()
		{
			if(Input.IsButtonPressed(MouseButtons.Right))
			{
				base.Update();

				if (Monster == null)
					return;

				Monster m;

				var t = Monster.GetType();
				if (t == typeof(Slime))
				{
					m = new Slime();
				}
				else
				{
					return;
				}

				this.Scene.Add(m);
				
				m.Lua.Content = Monster.Lua.Content;

				m.GridPos = Grid.WorldToPoint(Input.MousePosition(this.Scene.Cam));

				BattleManager.Monsters.Add(m);
			}
		}
	}
}

