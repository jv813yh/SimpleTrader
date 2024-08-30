using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authentificators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.VVM.ViewModels.Factories.Interfaces;
using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class MainViewModel 
    {
        private readonly IRootSimpleTraderViewModelFactory _viewModelFactory;

        public INavigator Navigator { get; set; }
        public IAuthenticator Authenticator { get; set; }
        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator, 
            IAuthenticator authenticator,
            IRootSimpleTraderViewModelFactory viewModelFactory)
        {
            Navigator = navigator;
            Authenticator = authenticator;
            _viewModelFactory = viewModelFactory;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);

            // Set the default view model to HomeViewModel
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
