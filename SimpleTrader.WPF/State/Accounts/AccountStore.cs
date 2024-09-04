using SimpleTrader.Domain.Models;

namespace SimpleTrader.WPF.State.Accounts
{
    public class AccountStore : IAccountStore
    {
        public Account? CurrentAccount { get ; set; }
    }
}
