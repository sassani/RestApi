using Castle.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_V0.Core.Security
{
	public class AppRSA
	{
		public IConfiguration Configuration { get; }
		public AppRSA(IConfiguration configuration)
		{
			Configuration = configuration;
		}



	}
}
