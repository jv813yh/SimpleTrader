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
        public List<ComboBoxOption> ComboBoxOptions { get; private set; }

        private ChartValues<double> _collectionForChartWithAmounts;
        public ChartValues<double> CollectionForChartWithAmounts 
        {
            get => _collectionForChartWithAmounts;
            set
            {
                _collectionForChartWithAmounts = value;
                OnPropertyChanged(nameof(CollectionForChartWithAmounts));
            }
        }

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

        private ObservableCollection<string> _collectionForChartWithNames;
        public ObservableCollection<string> CollectionForChartWithNames 
        { 
            get => _collectionForChartWithNames;
            set
            {
                _collectionForChartWithNames = value;
                OnPropertyChanged(nameof(CollectionForChartWithNames));
            }
        }

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

        public string SetErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }
        public MessageViewModel ErrorMessageViewModel { get; }

        public ICommand AmountOfAssetsOrMoneyCommand { get; }
        public ICommand SetColumnSeriesCommand { get; }

        public PortfolioViewModel(AssetStore assetStore)
        {
            ComboBoxOptions  = new List<ComboBoxOption>
            {
                new ComboBoxOption { Text = "Top", ImageSource = "/Images/3icon.png" , Number = 3 },
                new ComboBoxOption { Text = "Top", ImageSource = "/Images/10icon.png", Number = 10 },
                new ComboBoxOption { Text = "Top", ImageSource = "/Images/100icon.png", Number = 100 },
                new ComboBoxOption { Text = "All  ", ImageSource = "/Images/checkIcon.png", Number = -1 },
            };

            CollectionForChartWithAmounts  = new ChartValues<double>();
            CollectionForChartWithNames = new ObservableCollection<string>();

            AmountOfAssetsOrMoneyCommand = new RelayCommand(SetAmountOfAssetsOrMoneyCommand);
            SetColumnSeriesCommand = new SetColumnSeriesCommand(this, assetStore);


            ErrorMessageViewModel = new MessageViewModel();
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
                else if (commandValue == "assets")
                {
                    IsAccordingAmountOfAssets = true;
                }
            }
        }
    }
}
