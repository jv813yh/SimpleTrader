using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.VVM.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace SimpleTrader.WPF.Commands
{
    public class BuyStockCommand : BaseCommand
    {
        private readonly BuyViewModel _buyViewModel;
        private readonly IBuyStockService _buyStockService;
        private readonly IAccountStore _accountStore;

        public BuyStockCommand(BuyViewModel buyViewModel, 
            IBuyStockService buyStockService,
            IAccountStore accountStore)
        {
            _buyViewModel = buyViewModel;
            _buyStockService = buyStockService;
            _accountStore = accountStore;

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
        public override async void Execute(object? parameter)
        {
            try
            {
                // Buy the stock and return the account after the purchase
                Account account = await _buyStockService.BuyStockAsync(_accountStore.CurrentAccount, _buyViewModel.Symbol.ToUpper(), _buyViewModel
                .ConvertSharesToBuy(_buyViewModel.SharesToBuy));

                // Update the current account with the balance after the purchase, transactions ...
                _accountStore.CurrentAccount = account;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex.Message}", "Error",
                                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
