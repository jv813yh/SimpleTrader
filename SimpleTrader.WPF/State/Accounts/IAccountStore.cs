using SimpleTrader.Domain.Models;

namespace SimpleTrader.WPF.State.Accounts
{
    public interface IAccountStore
    {
        Account? CurrentAccount { get; set; }
        event Action StateChanged;
    }
}
