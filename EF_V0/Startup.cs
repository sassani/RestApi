using EF_V0.Core.Services;
using EF_V0.Core.Services.Interfaces;
using EF_V0.DataBase.Persistence;
using EF_V0.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EF_V0
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddOptions();

			var appSettingSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettingsModel>(appSettingSection);
			AppSettingsModel appSettings = appSettingSection.Get<AppSettingsModel>();


			services.ConfigureDbMySql(appSettings);
			services.ConfigureCors(appSettings);
			services.ConfigureAuthentication(appSettings);
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddHttpContextAccessor();

			
			services.AddTransient<ITokenService, TokenService>();
			services.AddTransient<IClientService, ClientService>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IAuthService, AuthService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else if (env.IsEnvironment("DropDB"))
			{
				app.UseDeveloperExceptionPage();
				// Create DB in Dev mode
				using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
				{
					var context = serviceScope.ServiceProvider.GetRequiredService<ApiContext>();
					context.Database.EnsureDeleted();
					context.Database.EnsureCreated();
				}
			}
			else
			{
				app.UseHsts();
			}

			app.UseCors("CorsPolicy");
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseMvc();
		}
	}
}
