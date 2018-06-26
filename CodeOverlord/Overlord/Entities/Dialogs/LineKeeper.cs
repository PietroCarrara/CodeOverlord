using System;
using Prime;

namespace Overlord
{
	public class LineKeeper : Component
	{
		private new Dialog Owner;

		public Action OnDone;

		public override void Initialize()
		{
			base.Initialize();

			this.Owner = (Dialog) base.Owner;
		}

		public override void Update()
		{
			if (Input.IsButtonPressed(MouseButtons.Left) && Owner.CurrentFinished)
			{
				Owner.Next();
			}
		}

		public override void OnDestroy()
		{
			base.OnDestroy();

			this.OnDone?.Invoke();
		}
	}
}
