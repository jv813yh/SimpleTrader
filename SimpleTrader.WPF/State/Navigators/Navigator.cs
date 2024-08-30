using SimpleTrader.WPF.VVM.ViewModels;

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
    }
}
