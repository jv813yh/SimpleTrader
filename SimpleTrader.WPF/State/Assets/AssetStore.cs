using SimpleTrader.Domain.Models;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.VVM.ViewModels;

namespace SimpleTrader.WPF.State.Assets
{
    public class AssetStore
    {
        private readonly IAccountStore _accountStore;

        // AccountBalance property
        public double AccountBalance 
            => _accountStore.CurrentAccount?.Balance ?? 0;

        // AssetTransactions property
        public IEnumerable<AssetTransaction> AssetTransactions 
            => _accountStore.CurrentAccount?.AssetTransactions ?? new List<AssetTransaction>();

        public event Action StateChanged;

        public AssetStore(IAccountStore accountStore)
        {
            _accountStore = accountStore;

            _accountStore.StateChanged += OnStateChanged;
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke();
        }

        /// <summary>
        /// When takeCount is -1, it will return all assets
        /// </summary>
        /// <param name="takeCount"> Amount of asset </param>
        /// <returns> All assets ordered by descending </returns>
        public IEnumerable<AssetViewModel> GetAssetsOrderByDescending(int takeCount)
        {
            IEnumerable<AssetViewModel> returnAssetViewModel = AssetTransactions.GroupBy(s => s.Asset.Symbol)
                .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.SharesAmount : -a.SharesAmount)))
                .Where(a => a.Shares > 0)
                .OrderByDescending(s => s.Shares);

            return takeCount == -1 ? returnAssetViewModel : returnAssetViewModel.Take(takeCount);
        }

        //public IEnumerable<AssetViewModel> GetAssetsAccordngByMoneyByDescending(int takeCount)
        //{
        //    IEnumerable<AssetViewModel> returnAssetViewModel = AssetTransactions.GroupBy(s => s.Asset.Symbol)
        //        .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.SharesAmount : -a.SharesAmount)))
        //        .Where(a => a.Shares > 0)
        //        .OrderByDescending(s => s.Shares * s.CurrentPrice);

        //    return takeCount == -1 ? returnAssetViewModel : returnAssetViewModel.Take(takeCount);
        //}

        public int GetAmountOwnedBySymbol(string symbol)
        {
            return AssetTransactions.GroupBy(s => s.Asset.Symbol)
                  .Where(g => g.Key == symbol)
                  .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.SharesAmount : -a.SharesAmount)))
                  .Sum(a => a.Shares);
        }


    }
}
