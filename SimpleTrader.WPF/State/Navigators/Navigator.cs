using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.VVM.ViewModels;
using SimpleTrader.WPF.VVM.ViewModels.Factories.Interfaces;
using System.Windows.Input;

namespace SimpleTrader.WPF.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        // This is the current view model that is being displayed
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel 
        {   
            get => _currentViewModel;
            set
            {   
                _currentViewModel = value;
                // Notify the UI that the CurrentViewModel has changed
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        // This is the command that will be executed to update the current view model
        public ICommand UpdateCurrentViewModelCommand { get; set; }

        public Navigator(ISimpleTraderViewModelAbstractFactory viewModelFactory)
        {
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this, viewModelFactory);
        }
    }
}
