using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.AuthentificationServices.Interfaces
{
    public enum RegistrationResult
    {
        Success,
        PasswordDoNotMatch,
        EmailAlreadyExists,
        UsernameAlreadyExists,
        UsernameOrEmailOrPasswordIsEmpty,
        StartBalanceMustBePositive
    }
    public interface IAuthenticationServices
    {
        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword, double startBalance);
        Task<Account> Login(string username, string password);
    }
}
