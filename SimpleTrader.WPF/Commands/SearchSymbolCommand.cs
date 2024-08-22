using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.WPF.VVM.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleTrader.WPF.Commands
{
    public class SearchSymbolCommand : BaseCommand
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
        public override void Execute(object? parameter)
        {
            string symbolToUpper = _buyViewModel.Symbol.ToUpper();

            _stockPriceService.GetPriceAsync(symbolToUpper).ContinueWith(task =>
            {
                if (!task.IsFaulted)
                {
                    _buyViewModel.SearchResultSymbol = symbolToUpper;
                    _buyViewModel.PricePerShare = task.Result;

                    if (int.TryParse(_buyViewModel.SharesToBuy, out int shares))
                    {
                        if (shares > 0)
                        {
                            _buyViewModel.SharesToBuy = "0";
                        }
                    }
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
