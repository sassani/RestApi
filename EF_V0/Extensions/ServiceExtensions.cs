using EF_V0.DataBase;
using EF_V0.DataBase.Core;
using EF_V0.DataBase.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_V0.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
		{
			var connection = configuration.GetConnectionString("DbConnection");
			services.AddDbContext<ApiContext>(options => options
			///.UseLazyLoadingProxies()
			.UseSqlServer(connection));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}

		public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
		{
			string[] validOrigins = configuration.GetSection("CrossUrl").Get<string[]>();
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder
					//.WithOrigins(validOrigins)
					.AllowAnyOrigin() // TODO: change this before deployment!
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());
			});
		}
	}
}
