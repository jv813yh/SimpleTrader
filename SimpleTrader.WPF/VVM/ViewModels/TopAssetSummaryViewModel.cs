using SimpleTrader.WPF.State.Assets;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class TopAssetSummaryViewModel : BaseViewModel
    {
        private readonly AssetStore _assetStore;
        public double AccountBalance
            => _assetStore.AccountBalance;

        public AssetListingViewModel AssetListingViewModel { get; }

        public TopAssetSummaryViewModel(AssetStore assetStore)
        {
            _assetStore = assetStore;
            AssetListingViewModel = new AssetListingViewModel(_assetStore, 3);

            assetStore.StateChanged += AssetStoreChanged;
        }

        private void AssetStoreChanged()
        {
            // Raise the Property changed event for the AccountBalance property
            OnPropertyChanged(nameof(AccountBalance));
        }
    }
}
