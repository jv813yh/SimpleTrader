using SimpleTrader.Domain.Services.AuthentificationServices.Interfaces;
using SimpleTrader.WPF.State.Authentificators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.VVM.ViewModels;
using System.ComponentModel;

namespace SimpleTrader.WPF.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _loginRenavigator;

        public RegisterCommand(RegisterViewModel registerViewModel, 
            IAuthenticator authenticator,
            IRenavigator loginRenavigator)
        {
            _registerViewModel = registerViewModel;
            _authenticator = authenticator;
            _loginRenavigator = loginRenavigator;


            _registerViewModel.PropertyChanged += OnPropertyViewModelChanged;

        }

        private void OnPropertyViewModelChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_registerViewModel.Username) ||
                e.PropertyName == nameof(_registerViewModel.Email) ||
                e.PropertyName == nameof(_registerViewModel.Password) ||
                e.PropertyName == nameof(_registerViewModel.ConfirmPassword))
            {
                OnRaiseCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _registerViewModel.CanTryRegister && 
                   base.CanExecute(parameter);  
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            // Clear the error message
            _registerViewModel.SetErrorMessageViewModel = string.Empty;

            try
            {
                // Try to register the new user
                RegistrationResult result = await _authenticator.RegisterAsync(_registerViewModel.Email,
                                       _registerViewModel.Username,
                                       _registerViewModel.Password,
                                       _registerViewModel.ConfirmPassword,
                                       500.0);

                // Action for the result of the registration
                ActionForRegistrationResult(result, _loginRenavigator);
            }
            catch (Exception)
            {
                _registerViewModel.SetErrorMessageViewModel = "Registration failed";
            }
        }

        private void ActionForRegistrationResult(RegistrationResult result, IRenavigator registerRenavigator)
        {
            switch (result)
            {
                case RegistrationResult.Success:
                     registerRenavigator.Renavigate();
                    break;
                case RegistrationResult.PasswordDoNotMatch:
                    _registerViewModel.SetErrorMessageViewModel = "Password does not match witih confirm password.";
                    break;
                case RegistrationResult.EmailAlreadyExists:
                    _registerViewModel.SetErrorMessageViewModel = "An account for this email already exists.";
                    break;
                case RegistrationResult.UsernameAlreadyExists:
                    _registerViewModel.SetErrorMessageViewModel = "An account for this username already exists";
                    break;
                case RegistrationResult.StartBalanceMustBePositive:
                    _registerViewModel.SetErrorMessageViewModel = "Start balance must be positive";
                    break;
                default:
                    _registerViewModel.SetErrorMessageViewModel = "Registration failed";
                    break;
            }
        }
    }
}
