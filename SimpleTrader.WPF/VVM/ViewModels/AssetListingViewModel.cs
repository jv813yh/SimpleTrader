using SimpleTrader.Domain.Models;
using SimpleTrader.WPF.State.Assets;
using System.Collections.ObjectModel;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class AssetListingViewModel : BaseViewModel
    {
        private readonly AssetStore _assetStore;
        private readonly ObservableCollection<AssetViewModel> _assets;

        public IEnumerable<AssetViewModel> Assets
            => _assets;

        //public AssetListingViewModel(AssetStore assetStore) : this(assetStore, assetStore => assets)
        //{
        //    _assets = assets;
        //}

        public AssetListingViewModel(AssetStore assetStore)
        {
            _assetStore = assetStore;
            _assets = new ObservableCollection<AssetViewModel>();

            _assetStore.StateChanged += OnStateChanged;

            ResetAssets();
        }

        private void OnStateChanged()
        {
            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssetViewModel> selectedAssets = _assetStore.AssetTransactions
                .GroupBy(s => s.Asset.Symbol)
                .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.SharesAmount : -a.SharesAmount)))
                .Where(a => a.Shares > 0)
                .OrderByDescending(s => s.Shares);

            _assets.Clear();
            foreach (var asset in selectedAssets)
            {
                _assets.Add(asset);
            }
        }
    }
}
