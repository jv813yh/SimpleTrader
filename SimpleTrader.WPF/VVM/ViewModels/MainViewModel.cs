using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authentificators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.VVM.ViewModels.Factories;
using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class MainViewModel 
    {
        private readonly ISimpleTraderViewModelFactory _viewModelFactory;

        // Navigator is uses for binding the current view model to the UI
        public INavigator Navigator { get; set; }

        // According authenticator in the Window, 
        // I show navigation bar, so is public
        public IAuthenticator Authenticator { get; set; }

        // UpdateCurrentViewModelCommand is uses for updating the current view model,
        // is binding in the MainWindow, so is public
        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator, 
            IAuthenticator authenticator,
            ISimpleTraderViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
            Navigator = navigator;
            Authenticator = authenticator;

            // Set the UpdateCurrentViewModelCommand with the navigator and the view model factory
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);

            // Set the default view model to LoginViewModel
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
