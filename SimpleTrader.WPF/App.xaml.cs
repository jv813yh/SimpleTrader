using SimpleTrader.Common.Interfaces;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.Domain.Services.Interfaces.TransactionServices;
using SimpleTrader.Domain.Services.TransactionProviders;
using SimpleTrader.EntityFramework.DbContexts;
using SimpleTrader.EntityFramework.Repositories;
using SimpleTrader.FinancialModelingAPI.Services;
using SimpleTrader.WPF.VVM.ViewModels;
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

            ICommonRepository<Account> repositoryAccount = new AccountRepository(new DesignTimeSimpleTraderDbContextFactory());
            IStockPriceService stockPriceService = new StockPriceProvider();
            IBuyStockService buyStockService = new BuyStockProvider(stockPriceService, repositoryAccount);

            Account AccountById4 = await repositoryAccount.GetByIdAsync(1);

            Account returnAccount = await buyStockService.BuyStockAsync(AccountById4, "T", 2);

            // Create the startup window
            Window window = new MainWindow()
            {
                // Set the data context of the window to the MainViewModel
                DataContext = new MainViewModel()
            };

            window.Show();

            base.OnStartup(e);
        }
    }
}
