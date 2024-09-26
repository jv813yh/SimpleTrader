using SimpleTrader.WPF.State.Assets;
using System.Collections.ObjectModel;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class AssetSummaryViewModel : BaseViewModel
    {
        private readonly AssetStore _assetStore;
        public double AccountBalance 
            => _assetStore.AccountBalance;

        public AssetListingViewModel AssetListingViewModel { get; }

        public AssetSummaryViewModel(AssetStore assetStore)
        {
            _assetStore = assetStore;
            AssetListingViewModel = new AssetListingViewModel(_assetStore);

            assetStore.StateChanged += AssetStoreChanged;
        }

        private void AssetStoreChanged()
        {
            // Raise the Property changed event for the AccountBalance property
            OnPropertyChanged(nameof(AccountBalance));
        }
    }
}
