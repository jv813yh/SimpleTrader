using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.VVM.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace SimpleTrader.WPF.Commands
{
    public class BuyStockCommand : AsyncCommandBase
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
         => _buyViewModel.CanBuyStock &&
            base.CanExecute(parameter);

        /// <summary>
        /// Override the method to execute the command to buy the stock
        /// </summary>
        /// <param name="parameter"></param>
        public override async Task ExecuteAsync(object? parameter)
        {
            // Set the status and error messages to empty
            _buyViewModel.SetStatusMessage = string.Empty;
            _buyViewModel.SetErrorMessage = string.Empty;

            try
            {
                // Buy the stock and return the account after the purchase
                Account account = await _buyStockService.BuyStockAsync(_accountStore.CurrentAccount, _buyViewModel.Symbol.ToUpper(), _buyViewModel
                .ConvertSharesToBuy(_buyViewModel.SharesToBuy));

                // Update the current account with the balance after the purchase, transactions ...
                _accountStore.CurrentAccount = account;

                // Show a message box to inform the user that the purchase was successful
                _buyViewModel.SetStatusMessage = $"Successfully bought {_buyViewModel.SharesToBuy} shares of {_buyViewModel.Symbol.ToUpper()}";
            }
            catch (Exception ex)
            {
                // Inform the user that the purchase was not successful
                _buyViewModel.SetErrorMessage = ex.Message;    
            }
        }
    }
}
