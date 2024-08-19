namespace SimpleTrader.WPF.VVM.ViewModels.Factories.Interfaces
{
    public interface ISimpleTraderViewModelFactory<T> where T : BaseViewModel
    {
        T CreateViewModel();
    }
}
