using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authentificators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.VVM.ViewModels.Factories;
using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ISimpleTraderViewModelFactory _viewModelFactory;
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigatorToLogin;

        // Navigator is uses for binding the current view model to the UI
        public BaseViewModel CurrentViewModel 
            => _navigator.CurrentViewModel;

        // According authenticator in the Window, 
        // I show navigation bar, so is public
        public bool IsLoggedIn 
            => _authenticator.IsLoggedIn;

        // UserName
        public string UserName
        {
            get => IsLoggedIn ? _authenticator.CurrentAccount.AccountHolder.Username : string.Empty;
        }

        // UpdateCurrentViewModelCommand is uses for updating the current view model,
        // is binding in the MainWindow, so is public
        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand LogOutCommand { get; }

        public MainViewModel(INavigator navigator, 
            IAuthenticator authenticator,
            ISimpleTraderViewModelFactory viewModelFactory,
            IRenavigator renavigatorToLogin)
        {
            _viewModelFactory = viewModelFactory;
            _navigator = navigator;
            _authenticator = authenticator;
            _renavigatorToLogin = renavigatorToLogin;

            // Subscribe to the StateChanged event of the navigator
            _navigator.StateChanged += Navigator_StateChanged;
            // Subscribe to the StateChanged event of the authenticator
            _authenticator.StateChanged += Authenticator_StateChanged;

            // Set the UpdateCurrentViewModelCommand with the navigator and the view model factory
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);

            // Set the LogOutCommand with the authenticator and the renavigato
            LogOutCommand = new LogOutCommand(authenticator, _renavigatorToLogin, this);

            // Set the default view model to LoginViewModel
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
            _renavigatorToLogin = renavigatorToLogin;

        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(UserName));
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        public override void Dispose()
        {
            _navigator.StateChanged -= Navigator_StateChanged;
            _authenticator.StateChanged -= Authenticator_StateChanged;
            base.Dispose();
        }
    }
}
