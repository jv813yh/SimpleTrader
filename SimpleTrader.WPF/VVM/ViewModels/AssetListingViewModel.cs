using SimpleTrader.WPF.State.Assets;
using System.Collections.ObjectModel;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class AssetListingViewModel : BaseViewModel
    {
        private readonly int _assetsCount;
        private readonly AssetStore _assetStore;
        private readonly ObservableCollection<AssetViewModel> _assets;

        public IEnumerable<AssetViewModel> Assets
            => _assets;

        public AssetListingViewModel(AssetStore assetStore, int assetsCount = -1)
        {
            _assetStore = assetStore;
            _assetsCount = assetsCount;
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
            // Get all assets and add them to the collection
            IEnumerable<AssetViewModel> selectedAssets = _assetStore.GetAssetsOrderByDescending(_assetsCount);

            DisposeAssets();
            _assets.Clear();

            foreach (var asset in selectedAssets)
            {
                _assets.Add(asset);
            }
        }

        private void DisposeAssets()
        {
            foreach(AssetViewModel assetViewModel in _assets)
            {
                assetViewModel.Dispose();
            }
        }

        public override void Dispose()
        {
            _assetStore.StateChanged -= OnStateChanged;
            DisposeAssets();
            base.Dispose();
        }
    }
}
