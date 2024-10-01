using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authentificators;
using SimpleTrader.WPF.State.Navigators;
using System.Collections;
using System.ComponentModel;
using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class RegisterViewModel : BaseViewModel, INotifyDataErrorInfo
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

                // Remove errors from the property and check validation
                RemoveErrors(nameof(Username));
                if (string.IsNullOrEmpty(value))
                {
                    AddError(nameof(Username), "Username cannot be empty");
                }
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

                // Remove errors from the property and check validation
                RemoveErrors(nameof(Email));
                if (string.IsNullOrEmpty(value))
                {
                    AddError(nameof(Email), "Email cannot be empty");
                }
                else if (!value.Contains("@"))
                {
                    AddError(nameof(Email), "Email must contain @");
                }
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

                // Remove errors from the property and check validation
                RemoveErrors(nameof(Password));
                if(string.IsNullOrEmpty(value))
                {
                    AddError(nameof(Password), "Password cannot be empty");
                }
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

                // Remove errors from the property and check validation
                RemoveErrors(nameof(ConfirmPassword));
                if (string.IsNullOrEmpty(value))
                {
                    AddError(nameof(ConfirmPassword), "Confirm password cannot be empty");
                }
                else if (value != Password)
                {
                    AddError(nameof(ConfirmPassword), "Password and Confirm password must match");
                }
            }
        }

        public bool CanTryRegister
        {
            get
            {
                return !string.IsNullOrEmpty(Username) && 
                       !string.IsNullOrEmpty(Email) &&
                       Email.Contains("@") &&
                       Password == ConfirmPassword &&
                       _startingBalanceIsNumber &&
                       !string.IsNullOrEmpty(Password) && 
                       !string.IsNullOrEmpty(StartingBalance) && 
                       !string.IsNullOrEmpty(ConfirmPassword);
            }
        }

        private bool _startingBalanceIsNumber;
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
                    _startingBalance = string.Empty;
                    _startingBalanceIsNumber = false;
                    return;
                }

                _startingBalance = value;
                _startingBalanceIsNumber = true;
                OnPropertyChanged(nameof(StartingBalance));

                // Remove errors from the property and check validation
                RemoveErrors(nameof(StartingBalance));
                if (string.IsNullOrEmpty(value))
                {
                    AddError(nameof(StartingBalance), "Starting balance cannot be empty");
                }
            }
        }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        
        private Dictionary<string, List<string>> _propertiesErrorsMessages = new Dictionary<string, List<string>>();

        private void AddError(string propertyName, string error)
        {
            // Verify if the property already has List of errors
            if (!_propertiesErrorsMessages.ContainsKey(propertyName))
            {
                _propertiesErrorsMessages[propertyName] = new List<string>();
            }

            if (!_propertiesErrorsMessages[propertyName].Contains(error))
            {
                _propertiesErrorsMessages[propertyName].Add(error);
                // Invoke event that errors were changed
                OnErrorsChanged(propertyName);
            }
        }

        private void RemoveErrors(string propertyName)
        {
            _propertiesErrorsMessages.Remove(propertyName);
            // Invoke event that errors were changed
            OnErrorsChanged(propertyName);
        }

        public MessageViewModel ErrorMessageViewModel { get; }
        public string SetErrorMessageViewModel
        { 
            set => ErrorMessageViewModel.Message = value; 
        }

        public ICommand RegisterCommand { get; }
        public ICommand ViewLoginNavigateCommand { get; }

        public bool HasErrors => 
            _propertiesErrorsMessages.Any();

        public RegisterViewModel(IRenavigator loginNavigator, 
                                 IAuthenticator authenticator)
        {
            ErrorMessageViewModel = new MessageViewModel();

            ViewLoginNavigateCommand= new NavigateCommand(loginNavigator);
            RegisterCommand = new RegisterCommand(this, authenticator, loginNavigator);
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertiesErrorsMessages.GetValueOrDefault(propertyName ?? string.Empty, new List<string>());
        }
    }
}
