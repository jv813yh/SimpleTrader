using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthentificationServices.Interfaces;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Authentificators;

namespace SimpleTrader.WPF.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        // Service from domain layer to authenticate the user with the database
        private readonly IAuthenticationServices _authentificationService;
        // Store the one current account for whole application
        private readonly IAccountStore _accountStore;

        public Authenticator(IAuthenticationServices authentificationService, IAccountStore accountStore)
        {
            _authentificationService = authentificationService;
            _accountStore = accountStore;
        }

        public Account? CurrentAccount
        {
            get => _accountStore.CurrentAccount;
            private set
            {
                // Set the current account 
                _accountStore.CurrentAccount = value;
                // Notify the subscribers that the state has changed
                OnStateChanged();
            }
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke();
        }

        public bool IsLoggedIn
            => CurrentAccount != null;

        public event Action StateChanged;

        /// <summary>
        /// Async Login the user with the given username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task LoginAsync(string username, string password)
        {
            // If the login is successful,
            // the CurrentAccount will be set to the account that was logged in
            CurrentAccount = await _authentificationService.Login(username, password);
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
        public async Task<RegistrationResult> RegisterAsync(string email, string username, string password, string confirmPassword, double startingBalance)
         => await _authentificationService.Register(email, username, password, confirmPassword, startingBalance);   
    }
}
