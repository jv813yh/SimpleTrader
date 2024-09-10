using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.WPF.VVM.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleTrader.WPF.Commands
{
    public class SearchSymbolCommand : AsyncCommandBase
    {
        private readonly BuyViewModel _buyViewModel;
        private readonly IStockPriceService _stockPriceService;

        public SearchSymbolCommand(BuyViewModel viewModel, IStockPriceService stockPriceService)
        {
            _buyViewModel = viewModel;
            _stockPriceService = stockPriceService;

            // Subscribe to the PropertyChanged event of the ViewModel property
            _buyViewModel.PropertyChanged += OnPropertyViewModelChanged;
        }

        private void OnPropertyViewModelChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_buyViewModel.Symbol))
            {
                OnRaiseCanExecuteChanged();
            }
        }

        // Check if the symbol is not empty
        public override bool CanExecute(object? parameter)
         => !string.IsNullOrEmpty(_buyViewModel.Symbol);

        // 
        public override async Task ExecuteAsync(object? parameter)
        {
            string symbolToUpper = _buyViewModel.Symbol.ToUpper();

            try
            {
                // Get the price of the stock symbol from the API
                // and update the ViewModel
                _buyViewModel.PricePerShare = await _stockPriceService.GetPriceAsync(symbolToUpper);
                _buyViewModel.SearchResultSymbol = symbolToUpper;

                if (int.TryParse(_buyViewModel.SharesToBuy, out int shares))
                {
                    if (shares > 0)
                    {
                        _buyViewModel.SharesToBuy = "0";
                    }
                }
            }
            catch (InvalidSymbolException)
            {
                _buyViewModel.SetErrorMessage = "Symbol does not exist";
            }
            catch (Exception)
            {
                _buyViewModel.SetErrorMessage = "Failed to load stock information";
            }
        }
    }
}
