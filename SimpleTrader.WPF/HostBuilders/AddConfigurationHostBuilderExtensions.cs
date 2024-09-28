using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddConfigurationHostBuilderExtensions
    {
        /// <summary>
        /// Extension method to add the appsettings.json file and the environment variables to the configuration
        /// 
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder AddConfiguration(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration(c =>
            {
                // Add the appsettings.json file and the environment variables to the configuration
                c.AddJsonFile("appsettings.json");
                c.AddEnvironmentVariables();
            });

            return hostBuilder;
        }
    }
}
