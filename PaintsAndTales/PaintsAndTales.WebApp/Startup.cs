using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaintsAndTales.Model;

namespace PaintsAndTales.WebApp
{
	public class Startup
	{
		public static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => { builder.AddConsole(); });

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddSession();

			DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
			DbContextOptions<ApplicationContext> options = optionsBuilder
				.UseMySql(Configuration.GetConnectionString("ApplicationDb"))
				.UseLoggerFactory(LoggerFactory)
				.Options;

			services.AddSingleton(options);

			services.AddDbContext<ApplicationContext>(ServiceLifetime.Transient, ServiceLifetime.Transient);
			services.AddDbContext<SoftDeletedApplicationContext>(ServiceLifetime.Transient, ServiceLifetime.Transient);
			services.Configure<ApplicationConfig>(Configuration.GetSection("ApplicationConfig"));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseStatusCodePagesWithRedirects("/Home/Error");
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseSession();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
