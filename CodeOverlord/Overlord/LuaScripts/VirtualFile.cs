using System;

namespace Overlord
{
	public class VirtualFile
	{
		private string text;
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (this.ReadOnly)
					throw new InvalidCodeException("Trying to modify a readonly file!");

				this.text = value;
			}
		}

		public bool ReadOnly, Spawnable;

		public VirtualFile(string s)
		{
			this.text = s;
		}
	}
}
