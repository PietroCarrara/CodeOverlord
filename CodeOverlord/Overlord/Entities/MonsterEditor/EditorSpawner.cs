using Prime;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class EditorSpawner : Entity
	{
		public override void Update()
		{
			base.Update();

			if(!Input.IsButtonPressed(MouseButtons.Middle))
				return;

			var pos = Grid.WorldToPoint(Input.MousePosition(this.Scene.Cam));

			var m = BattleManager.GetByPos(pos);

			if (m != null)
			{
				var e = this.Scene.Add(new Editor(m));
				e.Position = new Vector2(1280, 720) / 2;
			}
		}
	}
}
