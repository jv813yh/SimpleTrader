using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.EntityFramework.DbContexts;
using System.Configuration;

namespace SimpleTrader.WPF.HostBuilders
{
    /*
                    *    **************       DbContext      **************** 
    */
    public static class AddDbContextHostBuilderExtensions
    {
        /// <summary>
        /// Extension method to add the DbContext to the host builder
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                // Get the connection string from the configuration file and register the SimpleTraderDbContext
                string? connectionString = context.Configuration.GetConnectionString("SQLiteDefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ConfigurationErrorsException("DefaultConnection is not set");
                }
                // Set Action for the DbContextOptionsBuilder with the connection string
                Action<DbContextOptionsBuilder> optionsAction = options =>
                        options.UseSqlite(connectionString);

                services.AddDbContext<SimpleTraderDbContext>(optionsAction);
                // Register for the dbContext factory
                services.AddSingleton<SimpleTraderDbContextFactory>(new SimpleTraderDbContextFactory(optionsAction));
            });

            return host;
        }
    }    
}
