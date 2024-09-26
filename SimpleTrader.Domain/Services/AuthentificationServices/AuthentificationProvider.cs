using Microsoft.AspNet.Identity;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthentificationServices.Interfaces;
using SimpleTrader.Domain.Services.Interfaces;

namespace SimpleTrader.Domain.Services.AuthentificationServices
{
    public class AuthentificationProvider : IAuthenticationServices
    {
        // Service to interact with the database for the account
        private readonly IAccountService _accountService;
        // I use the PasswordHasher to hash the password before storing it in the database from ASP.NET Identity
        private readonly IPasswordHasher _passwordHasher;

        public AuthentificationProvider(IAccountService accountService, IPasswordHasher passwordHasher)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Account> Login(string username, string password)
        {
            // Get the account by the username
            Account? storedAccount = await _accountService.GetByUserName(username);

            if(storedAccount == null)
            {
                throw new UserNotFoundException(username);
            }

            // Verify the password
            var passwordResult = _passwordHasher.VerifyHashedPassword(storedAccount.AccountHolder.PasswordHash, password);

            // Check if the password is correct
            if (passwordResult == PasswordVerificationResult.Success)
            {
                return storedAccount;
            }
            else
            {
                throw new InvalidPasswordException(username, password);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <param name="startBalance"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword, double startingBalance)
        {
            // Check if the password and the confirm password are the same
            if (password != confirmPassword)
            {
                return RegistrationResult.PasswordDoNotMatch;
            }

            // Check if the email, username and password are not null or empty
            if (string.IsNullOrEmpty(email) || 
                string.IsNullOrEmpty(username) || 
                string.IsNullOrEmpty(password))
            {
                return RegistrationResult.UsernameOrEmailOrPasswordIsEmpty;
            }

            if(startingBalance < 0)
            {
                return RegistrationResult.StartingBalanceMustBePositive;
            }

            // Check if the Email is already used
            Account? storedAccountByEmail = await _accountService.GetByEmail(email);

            if (storedAccountByEmail != null)
            {
                return RegistrationResult.EmailAlreadyExists;
            }

            // Check if the UserName is already used
            Account? storedAccountByUserName = await _accountService.GetByUserName(username);

            if(storedAccountByUserName != null)
            {
                return RegistrationResult.UsernameAlreadyExists;
            }

            // Hash the password before storing it in the database
            string passwordHash = _passwordHasher.HashPassword(password);

            // Create a new account
            Account newAccount = new Account
            {
                AccountHolder = new User
                {
                    Email = email,
                    Username = username,
                    PasswordHash = passwordHash
                },

                Balance = startingBalance
            };

             // Create the account
            await _accountService.CreateAsync(newAccount);

            // Return the success result
            return RegistrationResult.Success;
        }
    }
}
