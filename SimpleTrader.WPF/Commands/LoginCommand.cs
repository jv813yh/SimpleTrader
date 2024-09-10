using SimpleTrader.Domain.Exceptions;
using SimpleTrader.WPF.State.Authentificators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.VVM.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(object? parameter)
        {
            if(parameter != null)
            {
                try
                {
                    // Verify the login credentials
                    await _authenticator.LoginAsync(_loginViewModel.Username, parameter.ToString());
                    // If the login is successful, navigate to the Home view
                    _renavigator.Renavigate();
                }
                catch (UserNotFoundException)
                {
                    _loginViewModel.SetErrorMessageViewModel = "Username does not exist";
                }
                catch (InvalidPasswordException)
                {
                    _loginViewModel.SetErrorMessageViewModel = "Password is incorrect";
                }
                catch (Exception)
                {
                    _loginViewModel.SetErrorMessageViewModel = "Login failed";
                }
            }
        }
    }
}
