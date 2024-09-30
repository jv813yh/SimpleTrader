namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public AssetSummaryViewModel AssetSummaryViewModel { get; }
        public MajorIndexListingViewModel MajorIndexListingViewModel { get;  }

        public HomeViewModel(MajorIndexListingViewModel majorIndexViewModel, AssetSummaryViewModel assetSummaryViewModel)
        {
            MajorIndexListingViewModel = majorIndexViewModel;
            AssetSummaryViewModel = assetSummaryViewModel;
        }

        public override void Dispose()
        {
            AssetSummaryViewModel?.Dispose();
            MajorIndexListingViewModel?.Dispose();

            base.Dispose();
        }
    }
}
