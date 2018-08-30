using System.Collections.Generic;
using Prime;
using System;

namespace Overlord
{
	public class Dialog : Entity
	{
		private List<Line> lines = new List<Line>();

		private IEnumerator<Line> linesE;

		public Action OnDone;

		public void Put(Line l)
		{
			lines.Add(l);

			if (this.Initialized)
			{
				this.Scene.Add(l);
				l.Reset();
				l.IsVisible = false;
			}
		}

		public override void Initialize()
		{
			base.Initialize();

			foreach (var l in lines)
			{
				this.Scene.Add(l);
				l.Reset();
				l.IsVisible = false;
			}
		}

		public bool CurrentFinished
		{
			get
			{
				if (linesE == null || linesE.Current == null)
					return true;

				return linesE.Current.IsFinished;
			}
		}

		public void Next()
		{
			if (linesE == null) return;

			if (linesE.Current != null)
			{
				linesE.Current.IsVisible = false;
			}

			if (linesE.MoveNext())
			{
				linesE.Current.IsVisible = true;
			}
			else
			{
				foreach (var line in lines)
				{
					line.Unatatch();
				}
				linesE = null;
				OnDone?.Invoke();
			}
		}

		public void Rewind()
		{
			linesE = lines.GetEnumerator();
			foreach (var line in lines)
			{
				line.Attach();
			}
		}

		public override void Update()
		{
			base.Update();

			if (Input.IsButtonPressed(MouseButtons.Left) && CurrentFinished)
			{
				Next();
			}
		}
	}
}
