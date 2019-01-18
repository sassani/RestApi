using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using EF_V0.DataBase;
using EF_V0.DataBase.Core;
using EF_V0.DataBase.Persistence;
using EF_V0.Extensions;

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
			services.ConfigureDb(Configuration);
			services.ConfigureCors(Configuration);
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
			app.UseMvc();
		}
	}
}
