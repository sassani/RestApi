using EF_V0.DataBase.Core.Domain;
using EF_V0.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace EF_V0.DataBase.Persistence
{
	public partial class ApiContext : DbContext
	{
		private readonly IOptions<AppSettingsModel> config;
		public ApiContext(DbContextOptions<ApiContext> options, IOptions<AppSettingsModel> config)
			: base(options)
		{
			this.config = config;
		}






	}
}