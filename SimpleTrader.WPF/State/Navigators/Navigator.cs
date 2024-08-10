using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.VVM.ViewModels;
using System.Windows.Input;

namespace SimpleTrader.WPF.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel 
        {   
            get => _currentViewModel;
            set
            {   
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand UpdateCurrentViewModelCommand
            => new UpdateCurrentViewModelCommand(this);
    }
}
