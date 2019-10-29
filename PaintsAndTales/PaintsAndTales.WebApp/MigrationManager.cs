using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaintsAndTales.Model;

namespace PaintsAndTales.WebApp
{
	public static class MigrationManager
	{
		private static readonly ILogger Logger = new LoggerFactory().CreateLogger(nameof(MigrationManager));

		public static IHost MigrateDatabase(this IHost webHost)
		{
			using (var scope = webHost.Services.CreateScope())
			{
				using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
				try
				{
					appContext.Database.Migrate();
				}
				catch (Exception ex)
				{
					Logger.LogError(ex, ex.Message);
					throw;
				}
			}

			return webHost;
		}
	}
}
