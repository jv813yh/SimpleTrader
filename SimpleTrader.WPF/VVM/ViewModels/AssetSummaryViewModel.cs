using SimpleTrader.WPF.State.Assets;
using System.Collections.ObjectModel;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class AssetSummaryViewModel : BaseViewModel
    {
        private readonly AssetStore _assetStore;
        private readonly ObservableCollection<AssetViewModel> _assets;

        public double AccountBalance 
            => _assetStore.AccountBalance;

        public IEnumerable<AssetViewModel> Assets 
            => _assets;


        public AssetSummaryViewModel(AssetStore assetStore)
        {
            _assetStore = assetStore;
            _assets = new ObservableCollection<AssetViewModel>();

            assetStore.StateChanged += OnStateChanged;
        }

        private void OnStateChanged()
        {
            // Raise the Property changed event for the AccountBalance property
            OnPropertyChanged(nameof(AccountBalance));

            // Clear the assets collection and calculate the new shares of assets 
            ResetAssets();
        }

        /// <summary>
        /// Calculate the new shares of assets
        /// </summary>
        private void ResetAssets()
        {
            // Group the asset transactions by the asset symbol and sum the shares amount
            IEnumerable<AssetViewModel> assetsViewModelCollection = _assetStore.AssetTransactions
                .GroupBy(t => t.Asset.Symbol)
                .Select(g => new AssetViewModel(g.Key, g.Sum(t => t.IsPurchase ? t.SharesAmount : -t.SharesAmount)))
                .Where(a => a.Shares > 0);

            // Clear the assets collection
            _assets.Clear();

            // Add the new assets to the collection
            foreach (var assetViewModel in assetsViewModelCollection)
            {
                _assets.Add(assetViewModel);
            }
        }
    }
}
