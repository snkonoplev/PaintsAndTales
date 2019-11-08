using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaintsAndTales.Model;

namespace PaintsAndTales.AdminApp
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
			DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
			DbContextOptions<ApplicationContext> options = optionsBuilder
				.UseMySql(Configuration.GetConnectionString("ApplicationDb"))
				.UseLoggerFactory(LoggerFactory)
				.Options;

			services.AddSingleton(options);

			services.AddDbContext<ApplicationContext>(ServiceLifetime.Transient, ServiceLifetime.Transient);
			services.AddDbContext<SoftDeletedApplicationContext>(ServiceLifetime.Transient, ServiceLifetime.Transient);
			services.AddControllers();
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "client-app/dist";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});

			app.UseRouting();

			app.UseAuthorization();

			app.UseStaticFiles();

			app.UseSpaStaticFiles();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "client-app";

				if (env.IsDevelopment())
				{
					spa.UseProxyToSpaDevelopmentServer("http://localhost:8080/");
				}
			});
		}
	}
}
