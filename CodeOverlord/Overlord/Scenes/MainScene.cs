using Prime;
using Prime.Graphics;

namespace Overlord
{
	public class MainScene : Scene
	{
		public override void Initialize()
		{
			base.Initialize();

			Add(new Demo());
		}
	}
}
