using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.Common.Interfaces;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.Domain.Services.Interfaces.TransactionServices;
using SimpleTrader.Domain.Services.TransactionProviders;
using SimpleTrader.EntityFramework.DbContexts;
using SimpleTrader.EntityFramework.Repositories;
using SimpleTrader.FinancialModelingAPI;
using SimpleTrader.FinancialModelingAPI.Services;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.VVM.ViewModels;
using SimpleTrader.WPF.VVM.ViewModels.Factories;
using SimpleTrader.WPF.VVM.ViewModels.Factories.Interfaces;
using System.Configuration;
using System.Windows;

namespace SimpleTrader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Create the service provider
            IServiceProvider serviceProvider = CreateServiceProvider();

            // Create the startup window
            Window window = new MainWindow()
            {
                DataContext = serviceProvider.GetRequiredService<MainViewModel>()
            };

            window.Show();

            base.OnStartup(e);
        }

        /// <summary>
        /// Get all the services that are needed for the application
        /// </summary>
        /// <returns> ServiceProvider containing services from our provider IServiceCollection </returns>
        private IServiceProvider CreateServiceProvider()
        {
            // Register the services in the service collection
            IServiceCollection serviceCollection = new ServiceCollection();

            // Get the API key from the configuration file and register the FinancialModelingHttpClientFactory
            string apiKey = ConfigurationManager.AppSettings.Get("financialApiKey");
            serviceCollection.AddSingleton<FinancialModelingHttpClientFactory>(new FinancialModelingHttpClientFactory(apiKey));

            // Register for the dbContext factory
            serviceCollection.AddSingleton<DesignTimeSimpleTraderDbContextFactory>();
            // Register the ICommonRepository<Account> and AccountRepository like Singleton service
            serviceCollection.AddSingleton<ICommonRepository<Account>, AccountRepository>();
            // Register IStockPriceService and StockPriceProvider like Singleton service
            serviceCollection.AddSingleton<IStockPriceService, StockPriceProvider>();
            // Register IBuyStockService and BuyStockProvider like Singleton service
            serviceCollection.AddSingleton<IBuyStockService, BuyStockProvider>();
            // Register the IMajorIndexService and MajorIndexProvider like Singleton service
            serviceCollection.AddSingleton<IMajorIndexService, MajorIndexProvider>();

            
            // Register the RootSimpleTraderViewModelFactory, HomeViewModelFactory,
            // MajorIndexListingViewModelFactory and PortfolioViewModelFactory like Singleton services
            serviceCollection.AddSingleton<IRootSimpleTraderViewModelFactory, RootSimpleTraderViewModelAbstractFactory>();
            serviceCollection.AddSingleton<ISimpleTraderViewModelFactory<HomeViewModel>, HomeViewModelFactory>();
            serviceCollection.AddSingleton<ISimpleTraderViewModelFactory<MajorIndexListingViewModel>, MajorIndexListingViewModelFactory>();
            serviceCollection.AddSingleton<ISimpleTraderViewModelFactory<PortfolioViewModel>, PortfolioViewModelFactory>();

            // Register the Navigator as a AddScoped service
            serviceCollection.AddScoped<INavigator, Navigator>();
            serviceCollection.AddScoped<BuyViewModel>();
            // Register the MainViewModel as a Scoped service
            serviceCollection.AddScoped<MainViewModel>();

            // Build the service provider
            return serviceCollection.BuildServiceProvider();
        }
    }
}
