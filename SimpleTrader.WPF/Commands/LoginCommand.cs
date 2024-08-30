using SimpleTrader.WPF.State.Authentificators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.VVM.ViewModels;
using SimpleTrader.WPF.VVM.ViewModels.Factories.Interfaces;

namespace SimpleTrader.WPF.Commands
{
    public class LoginCommand : BaseCommand
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;


        public LoginCommand(LoginViewModel loginViewModel, 
            IAuthenticator authenticator,
            IRenavigator renavigator)
        {
            _loginViewModel = loginViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;
        }


        public LoginCommand(LoginViewModel loginViewModel)
        {
            _loginViewModel = loginViewModel;
        }


        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        public override async void Execute(object? parameter)
        {
            if(parameter != null)
            {
                // Verify the login credentials
                bool success = await _authenticator.LoginAsync(_loginViewModel.Username, parameter.ToString());

                // If the login is successful, navigate to the Home view
                if(success)
                {
                    _renavigator.Renavigate();
                }
            }
        }
    }
}
