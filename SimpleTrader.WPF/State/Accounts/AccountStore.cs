using SimpleTrader.Domain.Models;

namespace SimpleTrader.WPF.State.Accounts
{
    public class AccountStore : IAccountStore
    {
        private Account? _currentAccount;
        public Account? CurrentAccount
        {
            get => _currentAccount;
            set
            {
                _currentAccount = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}
