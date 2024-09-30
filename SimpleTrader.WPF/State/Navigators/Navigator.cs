using SimpleTrader.WPF.VVM.ViewModels;

namespace SimpleTrader.WPF.State.Navigators
{
    public class Navigator : INavigator
    {
        // This is the current view model that is being displayed
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel 
        {   
            get => _currentViewModel;
            set
            {
                // Dispose the current view model, if it is not null
                _currentViewModel?.Dispose();

                _currentViewModel = value;
                // When the current view model is changed, the event is triggered
                OnStateChanged();
            }
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke(); 
        }

        public event Action StateChanged;
    }
}
