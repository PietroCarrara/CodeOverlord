using System.Collections.Generic;
using Prime;

namespace Overlord
{
	public class Dialog : Entity
	{
		private List<Line> lines = new List<Line>();

		private IEnumerator<Line> linesE;

		public void Put(Line l)
		{
			lines.Add(l);

			l.IsVisible = false;
		}

		public bool CurrentFinished
		{
			get
			{
				if (linesE.Current == null)
					return true;

				return linesE.Current.IsFinished;
			}
		}

		public void Next()
		{
			if (linesE == null)
			{
				linesE = lines.GetEnumerator();
			}
			else
			{
				linesE.Current.IsVisible = false;
			}

			if (linesE.MoveNext())
			{
				linesE.Current.IsVisible = true;
			}
			else
			{
				this.Destroy();
			}
		}
	}
}
