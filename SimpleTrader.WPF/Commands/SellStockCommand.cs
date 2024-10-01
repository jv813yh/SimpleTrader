using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.VVM.ViewModels;
using System.ComponentModel;

namespace SimpleTrader.WPF.Commands
{
    public class SellStockCommand : AsyncCommandBase
    {
        private readonly SellViewModel _sellViewModel;
        private readonly ISellStockService _sellStockService;
        private readonly IAccountStore _accountStore;

        public SellStockCommand(SellViewModel sellViewModel, 
            ISellStockService sellStockService,
            IAccountStore accountStore)
        {
            _sellViewModel = sellViewModel;
            _sellStockService = sellStockService;
            _accountStore = accountStore;

            // Subscribe to the PropertyChanged event of the BuyViewModel property has changed
            _sellViewModel.PropertyChanged += OnPropertyViewModelChanged;
        }

        private void OnPropertyViewModelChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_sellViewModel.CanSellStock))
            {
                OnRaiseCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
            => _sellViewModel.CanSellStock &&
               base.CanExecute(parameter);
        public async override Task ExecuteAsync(object? parameter)
        {
            string sellSymbolToUpper = _sellViewModel.Symbol.ToUpper();

            // Set the status and error messages to empty
            _sellViewModel.SetStatusMessage = string.Empty;
            _sellViewModel.SetErrorMessage = string.Empty;

            try
            {
                // Buy the stock and return the account after the purchase
                Account account = await _sellStockService.SellStockAsync(_accountStore.CurrentAccount, sellSymbolToUpper,
                    Convert.ToInt32(_sellViewModel.SharesToSell));

                // Update the current account with the balance after the purchase, transactions ...
                _accountStore.CurrentAccount = account;

                // Show a message box to inform the user that the purchase was successful
                _sellViewModel.SetStatusMessage = $"Successfully sold {_sellViewModel.SharesToSell} shares of {sellSymbolToUpper}";

                _sellViewModel.SearchResultSymbol = string.Empty;
            }
            catch (InvalidSymbolException)
            {
                // Inform the user that the purchase was not successful
                _sellViewModel.SetErrorMessage = "Symbol does not exist. ";
            }
            catch (InsufficientSharesException ex)
            {
                // Inform the user that the purchase was not successful
                _sellViewModel.SetErrorMessage = $"Account has insufficient shares. You only have {ex.AccountShares} shares.";
            }
            catch (Exception ex)
            {
                // Inform the user that the purchase was not successful
                _sellViewModel.SetErrorMessage = ex.Message;
            }
        }
    }
}
