using SimpleTrader.Common.Interfaces;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.Interfaces
{
    public interface IAccountService : ICommonRepository<Account>
    {
        Task<Account?> GetByEmail(string email);
        Task<Account?> GetByUserName(string username);
    }
}
