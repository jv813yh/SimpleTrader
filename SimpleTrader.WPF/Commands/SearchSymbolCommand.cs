using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.WPF.State.Assets;
using SimpleTrader.WPF.VVM.ViewModels;
using System.ComponentModel;

namespace SimpleTrader.WPF.Commands
{
    public class SearchSymbolCommand : AsyncCommandBase
    {
        private readonly AssetStore _assetStore;
        private readonly ISearchSymbolViewModel _viewModel;
        private readonly IStockPriceService _stockPriceService;

        public SearchSymbolCommand(ISearchSymbolViewModel viewModel, 
                                   IStockPriceService stockPriceService,
                                   AssetStore assetStore)
        {
            _viewModel = viewModel;
            _stockPriceService = stockPriceService;
            _assetStore = assetStore;

            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_viewModel.CanSearchSymbol))
            {
                OnRaiseCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.CanSearchSymbol &&
                base.CanExecute(parameter);
        }


        // 
        public override async Task ExecuteAsync(object? parameter)
        {
            string symbolToUpper = _viewModel.Symbol.ToUpper();

            try
            {
                // Get the price of the stock symbol from the API
                // and update the ViewModel
                _viewModel.SetStatusMessage = string.Empty;
                _viewModel.SetErrorMessage = string.Empty;
                _viewModel.PricePerShare = await _stockPriceService.GetPriceAsync(symbolToUpper);
                _viewModel.SearchResultSymbol = symbolToUpper;
                _viewModel.SharesOwned = Convert.ToString(_assetStore.GetAmountOwnedBySymbol(symbolToUpper));

            }
            catch (InvalidSymbolException)
            {
                _viewModel.SetErrorMessage = "Symbol does not exist";
            }
            catch (Exception)
            {
                _viewModel.SetErrorMessage = "Failed to load stock information";
            }
        }
    }
}
