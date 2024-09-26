using Newtonsoft.Json.Linq;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authentificators;
using SimpleTrader.WPF.State.Navigators;
using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _username;
        public string Username
        {             
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(CanTryRegister));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(CanTryRegister));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanTryRegister));
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
                OnPropertyChanged(nameof(CanTryRegister));
            }
        }

        public bool CanTryRegister
        {
            get
            {
                return !string.IsNullOrEmpty(Username) && 
                       !string.IsNullOrEmpty(Email) && 
                       !string.IsNullOrEmpty(Password) && 
                       !string.IsNullOrEmpty(StartingBalance) && 
                       !string.IsNullOrEmpty(ConfirmPassword);
            }
        }

        private string _startingBalance;
        public string StartingBalance
        {
            get => _startingBalance;
            set
            {
                SetErrorMessageViewModel = string.Empty;
                if (!Double.TryParse(value, out double result))
                {
                    SetErrorMessageViewModel = "Starting balance must be a number";
                    return;
                }

                _startingBalance = value;
                OnPropertyChanged(nameof(StartingBalance));
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; }
        public string SetErrorMessageViewModel
        { 
            set => ErrorMessageViewModel.Message = value; 
        }

        public ICommand RegisterCommand { get; }
        public ICommand ViewLoginNavigateCommand { get; }

        public RegisterViewModel(IRenavigator loginNavigator, 
                                 IAuthenticator authenticator)
        {
            ErrorMessageViewModel = new MessageViewModel();

            ViewLoginNavigateCommand= new NavigateCommand(loginNavigator);
            RegisterCommand = new RegisterCommand(this, authenticator, loginNavigator);
        }
    }
}
