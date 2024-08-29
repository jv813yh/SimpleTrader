using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthentificationServices.Interfaces;
using SimpleTrader.WPF.State.Authentificators;

namespace SimpleTrader.WPF.State.Authenticators
{
    public class Authenticator : ObservableObject, IAuthenticator
    {
        // Service from domain layer to authenticate the user with the database
        private readonly IAuthenticationServices _authentificationService;

        public Authenticator(IAuthenticationServices authentificationService)
        {
            _authentificationService = authentificationService;
        }

        private Account? _currentAccount;
        public Account? CurrentAccount
        {
            get => _currentAccount;
            private set
            {
                // Set the current account and notify the UI
                _currentAccount = value;
                OnPropertyChanged(nameof(CurrentAccount));
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        public bool IsLoggedIn
            => CurrentAccount != null;

        /// <summary>
        /// Async Login the user with the given username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> LoginAsync(string username, string password)
        {
            // If the login is successful,
            // the CurrentAccount will be set to the account that was logged in
            bool success = false;
            try
            {
                CurrentAccount = await _authentificationService.Login(username, password);
                success = true;
            }
            catch (Exception)
            {

                return success;
            }

            return success;
        }

        public void Logout()
         => CurrentAccount = null;

        /// <summary>
        /// Async method to register the user with the given email, username, password, confirmPassword and startBalance
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <param name="startBalance"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<RegistrationResult> RegisterAsync(string email, string username, string password, string confirmPassword, double startBalance)
         => await _authentificationService.Register(email, username, password, confirmPassword, startBalance);   
    }
}
