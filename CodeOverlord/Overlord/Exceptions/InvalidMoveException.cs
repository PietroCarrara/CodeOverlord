using System;

namespace Overlord
{
	public class InvalidMoveException : OverlordException
	{
		public InvalidMoveException(string m) : base(m)
		{  }
	}
}
