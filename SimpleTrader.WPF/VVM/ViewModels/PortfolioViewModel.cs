using LiveCharts;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.Results;
using SimpleTrader.WPF.State.Assets;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class PortfolioViewModel : BaseViewModel
    {
        public ObservableCollection<ComboBoxOption> ComboBoxOptions { get; private set; }

        public ChartValues<int> CollectionForChartWithAmounts { get; set; }

        private bool _isChartVisible;
        public bool IsChartVisible 
        { 
            get => _isChartVisible;
            set
            {
                _isChartVisible = value;
                OnPropertyChanged(nameof(IsChartVisible));
            }
        }

        public List<string> CollectionForChartWithNames { get; set; }

        private ComboBoxOption _selelctedOption;
        public ComboBoxOption SelectedOption
        {
            get => _selelctedOption;
            set
            {
                _selelctedOption = value;
                OnPropertyChanged(nameof(SelectedOption));
            }
        }

        public bool IsAccordingAmountOfAssets { get; private set; } = true;
        public bool IsPurchases { get; private set; } = false;

        private string _messageForXAxis;
        public string MessageForXAxis
        {
            get => _messageForXAxis;
            set
            {
                _messageForXAxis = value;
                OnPropertyChanged(nameof(MessageForXAxis));
            }
        }

        private string _messageForYAxis;
        public string MessageForYAxis
        {
            get => _messageForYAxis;
            set
            {
                _messageForYAxis = value;
                OnPropertyChanged(nameof(MessageForYAxis));
            }
        }


        public ICommand AmountOfAssetsOrMoneyCommand { get; }
        public ICommand PurchasesCommand { get; }
        public ICommand SetColumnSeriesCommand { get; }

        public PortfolioViewModel(AssetStore assetStore)
        {
            ComboBoxOptions  = new ObservableCollection<ComboBoxOption>
            {
                new ComboBoxOption { Text = "Top", ImageSource = "/Images/3icon.png" , Number = 3 },
                new ComboBoxOption { Text = "Top", ImageSource = "/Images/10icon.png", Number = 10 },
                new ComboBoxOption { Text = "Top", ImageSource = "/Images/100icon.png", Number = 100 },
            };

            CollectionForChartWithAmounts  = new ChartValues<int>();
            CollectionForChartWithNames = new List<string>();

            AmountOfAssetsOrMoneyCommand = new RelayCommand(SetAmountOfAssetsOrMoneyCommand);
            PurchasesCommand = new RelayCommand(SetPurchasesCommand);

            SetColumnSeriesCommand = new SetColumnSeriesCommand(this, assetStore);
        }

        private void SetPurchasesCommand(object obj)
        {
            if (obj != null && 
                obj is string commandValue)
            {
                if(commandValue == "purchases")
                {
                    IsPurchases = true;
                }
            }
        }

        private void SetAmountOfAssetsOrMoneyCommand(object obj)
        {
            if(obj != null)
            {
                string commandValue = obj.ToString();

                if (commandValue == "money")
                {
                    IsAccordingAmountOfAssets = false;
                }
            }
        }
    }
}
