using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces.TransactionServices;
using SimpleTrader.WPF.VVM.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace SimpleTrader.WPF.Commands
{
    public class BuyStockCommand : BaseCommand
    {
        private readonly BuyViewModel _buyViewModel;
        private readonly IBuyStockService _buyStockService;

        public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService)
        {
            _buyViewModel = buyViewModel;
            _buyStockService = buyStockService;

            // Subscribe to the PropertyChanged event of the BuyViewModel property has changed
            _buyViewModel.PropertyChanged += OnPropertyViewModelChanged;
        }

        private void OnPropertyViewModelChanged(object? sender, PropertyChangedEventArgs e)
        {
           if(e.PropertyName == nameof(_buyViewModel.SharesToBuy) || 
                             e.PropertyName == nameof(_buyViewModel.Symbol))
           {
                OnRaiseCanExecuteChanged();
           }
        }

        // Check if the command can be executed according to the veryfies
        public override bool CanExecute(object? parameter)
         => !string.IsNullOrEmpty(_buyViewModel.Symbol) && 
            _buyViewModel.ConvertSharesToBuy(_buyViewModel.SharesToBuy) > 0;

        /// <summary>
        /// Override the method to execute the command to buy the stock
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object? parameter)
        {
            Account firstAccount = new Account()
            {
                Id = 1,
                Balance = 500,
                AssetTransactions = new List<AssetTransaction>()
            };

            _buyStockService.BuyStockAsync(firstAccount, _buyViewModel.Symbol, _buyViewModel
                .ConvertSharesToBuy(_buyViewModel.SharesToBuy))
                .ContinueWith(task =>
            {
                if(!task.IsFaulted)
                {
                   Account buyAccount =  task.Result;
                }
                else
                {
                    MessageBox.Show($"{task.Exception.Message}", "Error",
                                               MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
    }
}
