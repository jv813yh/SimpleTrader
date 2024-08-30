namespace SimpleTrader.WPF.VVM.ViewModels.Factories.Interfaces
{
    public interface ISimpleTraderViewModelFactory<TViewModel> where TViewModel : BaseViewModel
    {
        TViewModel CreateViewModel();
    }
}
