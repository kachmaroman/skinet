using System;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
			IHost host = CreateHostBuilder(args).Build();

			using (IServiceScope scope = host.Services.CreateScope())
			{
				IServiceProvider services = scope.ServiceProvider;

				ILoggerFactory loggerFactory = services.GetRequiredService<ILoggerFactory>();

				try
				{
					StoreContext context = services.GetRequiredService<StoreContext>();
					await context.Database.MigrateAsync();
					await StoreContextSeed.SeedAsync(context, loggerFactory);

					UserManager<AppUser> userManager = services.GetRequiredService<UserManager<AppUser>>();
					AppIdentityDbContext identityContext = services.GetRequiredService<AppIdentityDbContext>();
					await identityContext.Database.MigrateAsync();
					await AppIdentityDbContextSeed.SeedUsersAsync(userManager);

				}
				catch (Exception ex)
				{
					var logger = loggerFactory.CreateLogger<Program>();
					logger.LogError(ex, "An error occured during migration");
				}
			}

			host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
	        Host.CreateDefaultBuilder(args)
		        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
