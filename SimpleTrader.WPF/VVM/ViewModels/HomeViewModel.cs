namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public MajorIndexViewModel MajorIndexViewModel { get; set; }


        public HomeViewModel(MajorIndexViewModel majorIndexViewModel)
        {
            MajorIndexViewModel = majorIndexViewModel;
        }
    }
}
