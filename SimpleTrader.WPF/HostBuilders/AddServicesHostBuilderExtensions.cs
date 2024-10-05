using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Common.Interfaces;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthentificationServices;
using SimpleTrader.Domain.Services.AuthentificationServices.Interfaces;
using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.Domain.Services.Interfaces.TransactionServices;
using SimpleTrader.Domain.Services.TransactionProviders;
using SimpleTrader.EntityFramework.Repositories;
using SimpleTrader.FinancialModelingAPI.Services;

namespace SimpleTrader.WPF.HostBuilders
{

    /*
                   *    **************       Services      **************** 
    */
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                // Register the ICommonRepository<Account> and AccountRepository like Singleton service
                services.AddSingleton<ICommonRepository<Account>, AccountRepository>();
                // Register the IAccountService and AccountRepository like Singleton service
                services.AddSingleton<IAccountService, AccountRepository>();
                // Register the ICommonRepository<Account> and AccountRepository like Singleton service
                services.AddSingleton<IAuthenticationServices, AuthentificationProvider>();
                // Register IStockPriceService and StockPriceProvider like Singleton service
                services.AddSingleton<IStockPriceService, StockPriceProvider>();
                // Register IBuyStockService and BuyStockProvider like Singleton service
                services.AddSingleton<IBuyStockService, BuyStockProvider>();
                // Register the IMajorIndexService and MajorIndexProvider like Singleton service
                services.AddSingleton<IMajorIndexService, MajorIndexProvider>();
                // Register the PasswordHasher like Singleton service for hashing
                services.AddSingleton<IPasswordHasher, PasswordHasher>();
                services.AddSingleton<ISellStockService, SellStockProvider>();
            });

            return host;
        }
    }
}
