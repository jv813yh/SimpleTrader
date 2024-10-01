using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.Domain.Services.Interfaces.TransactionServices;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Assets;
using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class BuyViewModel : BaseViewModel, ISearchSymbolViewModel
    {
        // Input symbol from user to buy
        private string _symbol = string.Empty;
        public string Symbol
        {
            get => _symbol;
            set
            {
                _symbol = value;
                OnPropertyChanged(nameof(Symbol));
                OnPropertyChanged(nameof(CanSearchSymbol));
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
                OnPropertyChanged(nameof(TotalPrice));
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
            }
        }

        // Convert the shares to buy to an integer
        public int ConvertSharesToBuy(string stringValue)
        {
            if (int.TryParse(stringValue, out int intValue))
            {
                return intValue;
            }

            return 0;
        }

        // Number of shares to buy
        private int _sharesToBuy;
        public string SharesToBuy
        {
            get => _sharesToBuy.ToString();
            set
            {
                _sharesToBuy = ConvertSharesToBuy(value);
                OnPropertyChanged(nameof(SharesToBuy));
                // Update the total price
                OnPropertyChanged(nameof(TotalPrice));
                OnPropertyChanged(nameof(CanBuyStock));
            }
        }

        // Shares owned by the user
        private string _sharesOwned;
        public string SharesOwned 
        {
            get => _sharesOwned;
            set
            {
                _sharesOwned = value;
                OnPropertyChanged(nameof(SharesOwned));
                OnPropertyChanged(nameof(CanBuyStock));
            }
        }

        // Total price of the shares to buy
        public double TotalPrice
        {
            get
             => ConvertSharesToBuy(SharesToBuy) * PricePerShare;
        }

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
        public ICommand SearchSymbolCommand { get; }
        public ICommand BuyStockCommand { get; }

        public bool CanBuyStock 
            => TotalPrice > 0;

        public bool CanSearchSymbol => 
            !string.IsNullOrEmpty(Symbol);

        public BuyViewModel(IStockPriceService stockPriceService,
            IBuyStockService buyStockService,
            IAccountStore accountStore,
            AssetStore assetStore)
        {

            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService, assetStore);
            BuyStockCommand = new BuyStockCommand(this, buyStockService, accountStore);

            StatusMessageViewModel = new MessageViewModel();
            ErrorMessageViewModel = new MessageViewModel();
        }
    }
}
