using Microsoft.VisualBasic.ApplicationServices;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services.AuthentificationServices.Interfaces;

namespace SimpleTrader.WPF.State.Authentificators
{
    public interface IAuthenticator
    {
        Account? CurrentAccount { get; }
        bool IsLoggedIn { get; }

        event Action StateChanged;

        Task<RegistrationResult> RegisterAsync(string email, string username, string password, string confirmPassword, double startBalance);

        /// <summary>
        /// Login to the application
        /// </summary>
        /// <param name="username"> The user name </param>
        /// <param name="password"> The user password </param>
        /// <exception cref="UserNotFoundException"> Throw if the user does not exist </exception>
        /// <exception cref="InvalidPasswordException"> Throw if the password is invalid </exception>
        /// <exception cref="Exception"> Throw if the login is failed </exception>
        Task LoginAsync(string username, string password);
        void Logout();
    }
}
