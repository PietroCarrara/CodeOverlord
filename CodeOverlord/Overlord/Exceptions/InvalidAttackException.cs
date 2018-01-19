using System;

namespace Overlord
{
	public class InvalidAttackException : OverlordException
	{
		public InvalidAttackException(string m) : base(m)
		{  }
	}
}

