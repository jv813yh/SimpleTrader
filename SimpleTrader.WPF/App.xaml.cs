using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32.SafeHandles;
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
using SimpleTrader.WPF.State.Assets;
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
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(c =>
                {
                    c.AddJsonFile("appsettings.json");
                    c.AddEnvironmentVariables();
                })
                .ConfigureServices((context , serviceCollection) =>
                {
                    /*
                      *    **************       Http      **************** 
                      */
                    // Get the API key from the configuration file and register the FinancialModelingHttpClientFactory
                    string? apiKey = context.Configuration.GetValue<string>("FINANCE_API_KEY");
                    if (string.IsNullOrEmpty(apiKey))
                    {
                        throw new ConfigurationErrorsException("financialApiKey is not set");
                    }
                    serviceCollection.AddSingleton<FinancialModelingHttpClientFactory>(new FinancialModelingHttpClientFactory(apiKey));

                    /*
                    *    **************       DbContext      **************** 
                    */

                    // Get the connection string from the configuration file and register the SimpleTraderDbContext
                    string? connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                    if(string.IsNullOrEmpty(connectionString))
                    {
                        throw new ConfigurationErrorsException("DefaultConnection is not set");
                    }
                    serviceCollection.AddDbContext<SimpleTraderDbContext>(options =>
                    {
                        options.UseSqlServer(connectionString);
                    });
                    // Register for the dbContext factory
                    serviceCollection.AddSingleton<DesignTimeSimpleTraderDbContextFactory>(new DesignTimeSimpleTraderDbContextFactory(connectionString));

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
                    serviceCollection.AddSingleton<AssetStore>();
                    serviceCollection.AddSingleton<INavigator, Navigator>();
                    serviceCollection.AddSingleton<IAuthenticator, Authenticator>();

                    /*
                     *    **************       View Models      **************** 
                     */

                    // Register the SimpleTraderViewModelFactory like Singleton service
                    serviceCollection.AddSingleton<ISimpleTraderViewModelFactory, SimpleTraderViewModelFactory>();
                    serviceCollection.AddSingleton<BuyViewModel>();
                    serviceCollection.AddSingleton<PortfolioViewModel>();
                    serviceCollection.AddSingleton<AssetSummaryViewModel>();
                    serviceCollection.AddSingleton<HomeViewModel>(servies =>
                         new HomeViewModel(
                                MajorIndexListingViewModel.CreateMajorIndexViewModel(
                                servies.GetRequiredService<IMajorIndexService>()),
                                servies.GetRequiredService<AssetSummaryViewModel>())
                    );


                    serviceCollection.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
                    serviceCollection.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();
                    serviceCollection.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();

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

                    serviceCollection.AddSingleton<CreateViewModel<RegisterViewModel>>(servies =>
                    {
                        return () => new RegisterViewModel(
                                servies.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
                                servies.GetRequiredService<IAuthenticator>());
                    });

                    // Register the CreateViewModel<BuyViewModel> like Singleton service
                    serviceCollection.AddSingleton<CreateViewModel<BuyViewModel>>(servies =>
                    {
                        return () => servies.GetRequiredService<BuyViewModel>();
                    });


                    //serviceCollection.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
                    serviceCollection.AddSingleton(services =>
                        new ViewModelDelegateRenavigator<HomeViewModel>(
                            services.GetRequiredService<INavigator>(),
                            services.GetRequiredService<CreateViewModel<HomeViewModel>>())
                    );

                    //serviceCollection.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
                        serviceCollection.AddSingleton(services =>
                            new ViewModelDelegateRenavigator<RegisterViewModel>(
                                services.GetRequiredService<INavigator>(),
                                services.GetRequiredService<CreateViewModel<RegisterViewModel>>()));

                    // Register the CreateViewModel<LoginViewModel> like Singleton service
                    // for creating LoginViewModel according the delegate function
                    serviceCollection.AddSingleton<CreateViewModel<LoginViewModel>>(services =>
                    {
                        return () => new LoginViewModel(
                            services.GetRequiredService<IAuthenticator>(),
                            services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
                            services.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>());
                    });

                    // Register the CreateViewModel<BuyViewModel> like Singleton service
                    serviceCollection.AddSingleton<CreateViewModel<BuyViewModel>>(servies =>
                    {
                        return () => servies.GetRequiredService<BuyViewModel>();
                    });
                });
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            // Start the host
            _host.Start();
            // Create the startup window
            Window window = new MainWindow()
            {
                DataContext = _host.Services.GetRequiredService<MainViewModel>()
            };

            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            // Stop the host
            await _host.StopAsync();
            // Dispose the host
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
