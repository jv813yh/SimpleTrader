using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.Common.Interfaces;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthentificationServices;
using SimpleTrader.Domain.Services.AuthentificationServices.Interfaces;
using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.Domain.Services.Interfaces.TransactionServices;
using SimpleTrader.Domain.Services.TransactionProviders;
using SimpleTrader.EntityFramework.DbContexts;
using SimpleTrader.EntityFramework.Repositories;
using SimpleTrader.FinancialModelingAPI;
using SimpleTrader.FinancialModelingAPI.Services;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Authentificators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.VVM.ViewModels;
using SimpleTrader.WPF.VVM.ViewModels.Factories;
using System.Configuration;
using System.Windows;

namespace SimpleTrader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
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

            /*
             *    **************       Http      **************** 
             */
            // Get the API key from the configuration file and register the FinancialModelingHttpClientFactory
            string? apiKey = ConfigurationManager.AppSettings.Get("financialApiKey");
            if(string.IsNullOrEmpty(apiKey))
            {
                throw new ConfigurationErrorsException("financialApiKey is not set");
            }
            serviceCollection.AddSingleton<FinancialModelingHttpClientFactory>(new FinancialModelingHttpClientFactory(apiKey));

            /*
            *    **************       DbContext      **************** 
            */

            // Register for the dbContext factory
            serviceCollection.AddSingleton<DesignTimeSimpleTraderDbContextFactory>();

            /*
             *    **************       Services      **************** 
             */

            // Register the ICommonRepository<Account> and AccountRepository like Singleton service
            serviceCollection.AddSingleton<ICommonRepository<Account>, AccountRepository>();
            // Register the IAccountService and AccountRepository like Singleton service
            serviceCollection.AddSingleton<IAccountService, AccountRepository>();
            // Register the ICommonRepository<Account> and AccountRepository like Singleton service
            serviceCollection.AddSingleton<IAuthenticationServices, AuthentificationProvider>();
            // Register IStockPriceService and StockPriceProvider like Singleton service
            serviceCollection.AddSingleton<IStockPriceService, StockPriceProvider>();
            // Register IBuyStockService and BuyStockProvider like Singleton service
            serviceCollection.AddSingleton<IBuyStockService, BuyStockProvider>();
            // Register the IMajorIndexService and MajorIndexProvider like Singleton service
            serviceCollection.AddSingleton<IMajorIndexService, MajorIndexProvider>();
            // Register the PasswordHasher like Singleton service for hashing
            serviceCollection.AddSingleton<IPasswordHasher, PasswordHasher>();
            // Register the IAcountStore as a Singleton service
            serviceCollection.AddSingleton<IAccountStore, AccountStore>();
            serviceCollection.AddSingleton<INavigator, Navigator>();
            serviceCollection.AddSingleton<IAuthenticator, Authenticator>();

            /*
             *    **************       View Models      **************** 
             */

            // Register the SimpleTraderViewModelFactory like Singleton service
            serviceCollection.AddSingleton<ISimpleTraderViewModelFactory, SimpleTraderViewModelFactory>();
            serviceCollection.AddSingleton<BuyViewModel>();
            serviceCollection.AddSingleton<PortfolioViewModel>();
            serviceCollection.AddSingleton<HomeViewModel>(servies =>
                 new HomeViewModel(
                        MajorIndexListingViewModel.CreateMajorIndexViewModel(
                        servies.GetRequiredService<IMajorIndexService>()))
            );

            // Register the MainViewModel as a Scoped service
            serviceCollection.AddScoped<MainViewModel>();
            // Register the CreateViewModel<HomeViewModel> like Singleton service
            // for creating HomeViewModel according the delegate function
            serviceCollection.AddSingleton<CreateViewModel<HomeViewModel>>(services =>
            {
                return () => services.GetRequiredService<HomeViewModel>();  
            });
            // Register the CreateViewModel<PortfolioViewModel> like Singleton service
            // for creating PortfolioViewModel according the delegate function
            serviceCollection.AddSingleton<CreateViewModel<PortfolioViewModel>>(services =>
            {
                return () => services.GetRequiredService<PortfolioViewModel>();
            });

            //serviceCollection.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
            serviceCollection.AddSingleton(services => 
                new ViewModelDelegateRenavigator<HomeViewModel>(
                    services.GetRequiredService<INavigator>(),
                    services.GetRequiredService<CreateViewModel<HomeViewModel>>())
            );

            // Register the CreateViewModel<LoginViewModel> like Singleton service
            // for creating LoginViewModel according the delegate function
            serviceCollection.AddSingleton<CreateViewModel<LoginViewModel>>(services =>
            {
                return () => new LoginViewModel(
                    services.GetRequiredService<IAuthenticator>(),
                    services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>());
            });

            // Register the CreateViewModel<BuyViewModel> like Singleton service
            serviceCollection.AddSingleton<CreateViewModel<BuyViewModel>>(servies =>
            {
                return () => servies.GetRequiredService<BuyViewModel>();
            });

            // Build the service provider
            return serviceCollection.BuildServiceProvider();
        }
    }
}
