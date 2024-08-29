using SimpleTrader.WPF.State.Authentificators;
using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class MainViewModel 
    {
        public INavigator Navigator { get; set; }
        public IAuthenticator Authenticator { get; set; }

        public MainViewModel(INavigator navigator, IAuthenticator authenticator)
        {
            Navigator = navigator;
            Authenticator = authenticator;

            // Set the default view model to HomeViewModel
            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
