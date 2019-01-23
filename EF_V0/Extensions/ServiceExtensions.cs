using EF_V0.DataBase;
using EF_V0.DataBase.Core;
using EF_V0.DataBase.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EF_V0.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureDb(this IServiceCollection services, AppSettings config)
		{

			services.AddDbContext<ApiContext>(options => options
			///.UseLazyLoadingProxies()
			.UseSqlServer(config.DbConnection));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}

		public static void ConfigureCors(this IServiceCollection services, AppSettings config)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder
					//.WithOrigins(config.CrossUrls)
					.AllowAnyOrigin() // TODO: change this before deployment!
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());
			});
		}

		public static void ConfigureAuthentication(this IServiceCollection services, AppSettings config)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(opt =>
			{
				opt.TokenValidationParameters = new TokenValidationParameters
				{
					RequireSignedTokens = true,
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidIssuer = config.Token.Issuer,
					ValidAudience = config.Token.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Token.SecretKey))
				};
			});
		}
	}
}
