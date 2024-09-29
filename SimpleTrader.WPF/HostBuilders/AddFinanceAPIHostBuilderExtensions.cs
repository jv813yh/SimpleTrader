using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.FinancialModelingAPI;
using SimpleTrader.FinancialModelingAPI.Models;
using System.Configuration;

namespace SimpleTrader.WPF.HostBuilders
{

    /*
                      *    **************       Http      **************** 
    */
    public static class AddFinanceAPIHostBuilderExtensions
    {
        public static IHostBuilder AddFinanceAPI(this IHostBuilder host)
        {
            host.ConfigureServices((context,services) =>
            {
                // Get the API key from the configuration file and register the FinancialModelingHttpClientFactory
                string? apiKey = context.Configuration.GetValue<string>("FINANCE_API_KEY");
                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new ConfigurationErrorsException("financialApiKey is not set");
                }

                services.AddSingleton(new FinancialModelingAPIKey(apiKey));

                services.AddHttpClient<FinancialModelingHttpClient>(client =>
                {
                    client.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
                });
            });

            return host;
        }
    }
}
