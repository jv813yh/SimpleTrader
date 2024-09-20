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
            // Get all assets and add them to the collection
            IEnumerable<AssetViewModel> selectedAssets = _assetStore.GetAssetsOrderByDescending(-1);

            _assets.Clear();

            foreach (var asset in selectedAssets)
            {
                _assets.Add(asset);
            }
        }
    }
}
