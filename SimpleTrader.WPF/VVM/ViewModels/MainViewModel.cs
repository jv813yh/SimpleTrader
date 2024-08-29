using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class MainViewModel 
    {
        public INavigator Navigator { get; set; }

        public MainViewModel(INavigator navigator)
        {
            Navigator = navigator;
            // Set the default view model to HomeViewModel
            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
