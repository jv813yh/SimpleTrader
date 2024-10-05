using SimpleTrader.WPF.State.Authentificators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.VVM.ViewModels;
using System.ComponentModel;

namespace SimpleTrader.WPF.Commands
{
    /// <summary>
    /// LogOutCommand is a command that logs out the user from the application
    /// and renavigates to the login view
    /// </summary>
    public class LogOutCommand : BaseCommand
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;
        private readonly MainViewModel _mainViewModel;

        public LogOutCommand(IAuthenticator authenticator, IRenavigator renavigator, MainViewModel mainViewModel)
        {
            _authenticator = authenticator;
            _renavigator = renavigator;
            _mainViewModel = mainViewModel;

            mainViewModel.PropertyChanged += OnMainViewModelPropertyChanged;
        }

        private void OnMainViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_mainViewModel.IsLoggedIn))
            {
                OnRaiseCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
         => _authenticator.IsLoggedIn;

        public override void Execute(object? parameter)
        {
            _authenticator.Logout();
            _renavigator.Renavigate();
        }
    }
}
