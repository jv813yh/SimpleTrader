namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public MajorIndexListingViewModel MajorIndexListingViewModel { get; set; }

        public HomeViewModel(MajorIndexListingViewModel majorIndexViewModel)
        {
            MajorIndexListingViewModel = majorIndexViewModel;
        }
    }
}
