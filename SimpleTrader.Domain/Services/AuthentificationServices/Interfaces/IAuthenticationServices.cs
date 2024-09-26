using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.Domain.Services.AuthentificationServices.Interfaces
{
    public enum RegistrationResult
    {
        Success,
        PasswordDoNotMatch,
        EmailAlreadyExists,
        UsernameAlreadyExists,
        UsernameOrEmailOrPasswordIsEmpty,
        StartingBalanceMustBePositive
    }
    public interface IAuthenticationServices
    {
        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword, double startingBalance);

        /// <summary>
        /// Get an account for user's credentials
        /// </summary>
        /// <param name="username"> The user name </param>
        /// <param name="password"> The user password </param>
        /// <returns> The account for the user </returns>
        /// <exception cref="UserNotFoundException"> Throw if the user does not exist </exception>
        /// <exception cref="InvalidPasswordException"> Throw if the password is invalid </exception>
        /// <exception cref="Exception"> Throw if the login is failed </exception>
        Task<Account> Login(string username, string password);
    }
}
