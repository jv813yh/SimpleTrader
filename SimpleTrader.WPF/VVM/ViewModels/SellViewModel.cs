using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.Domain.Services.Interfaces.TransactionServices;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Assets;
using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class SellViewModel : BaseViewModel, ISearchSymbolViewModel
    {
        private AssetViewModel _selectedAsset;
        public AssetViewModel SelectedAsset
        {
            get => _selectedAsset;
            set
            {
                _selectedAsset = value;
                OnPropertyChanged(nameof(SelectedAsset));
            }
        }

        // Current stock price of the symbol
        private double _pricePerShare;
        public double PricePerShare
        {
            get => _pricePerShare;
            set
            {
                _pricePerShare = value;
                OnPropertyChanged(nameof(PricePerShare));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private string _searchResultSymbol = string.Empty;
        public string SearchResultSymbol
        {
            get => _searchResultSymbol;
            set
            {
                _searchResultSymbol = value;
                OnPropertyChanged(nameof(SearchResultSymbol));
            }
        }

        public string _sharesOwned;
        public string SharesOwned
        {
            get => _sharesOwned;
            set
            {
                _sharesOwned = value;
                OnPropertyChanged(nameof(SharesOwned));
            }
        }

        // Input symbol from user to sell
        public string Symbol 
            => SelectedAsset?.Symbol;

        private int _sharesToSell;
        public int SharesToSell
        {
            get => _sharesToSell;
            set
            {
                _sharesToSell = value;
                OnPropertyChanged(nameof(SharesToSell));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
        public double TotalPrice 
            => SharesToSell * PricePerShare;

        public MessageViewModel StatusMessageViewModel { get; }
        public string SetStatusMessage
        {
            set => StatusMessageViewModel.Message = value;
        }
        public MessageViewModel ErrorMessageViewModel { get; }
        public string SetErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public AssetListingViewModel AssetListingViewModel { get; }

        public ICommand SearchSymbolCommand { get; }
        public ICommand SellStockCommand { get; }

        public SellViewModel(AssetStore assetStore, 
                             IStockPriceService stockPriceService, 
                             ISellStockService sellStockService, 
                             IAccountStore accountStore)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore);

            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            SellStockCommand = new SellStockCommand(this, sellStockService, accountStore);

            StatusMessageViewModel = new MessageViewModel();
            ErrorMessageViewModel = new MessageViewModel(); 
        }

    }
}
