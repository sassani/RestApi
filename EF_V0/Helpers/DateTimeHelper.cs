using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_V0.Helpers
{
	public class DateTimeHelper
	{
		public static long GetUnixTimestamp(DateTime dateTime)
		{
			DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime);
			return dateTimeOffset.ToUnixTimeSeconds();
		}
	}
}
