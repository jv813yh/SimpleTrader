using Microsoft.VisualBasic.ApplicationServices;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthentificationServices.Interfaces;

namespace SimpleTrader.WPF.State.Authentificators
{
    public interface IAuthenticator
    {
        Account? CurrentAccount { get; }
        bool IsLoggedIn { get; }

        event Action StateChanged;

        Task<RegistrationResult> RegisterAsync(string email, string username, string password, string confirmPassword, double startBalance);
        Task<bool> LoginAsync(string username, string password);
        void Logout();
    }
}
