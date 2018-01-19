using Prime;

namespace Overlord
{
	public class MonsterSpawner : Entity
	{
		public override void Update()
		{
			if(Input.IsButtonPressed(MouseButtons.Right))
			{
				var m = new Slime();
				m.Pos = Grid.WorldToPoint(Input.MousePosition(this.Scene.Cam));

				this.Scene.Add(m);
			}
		}
	}
}

