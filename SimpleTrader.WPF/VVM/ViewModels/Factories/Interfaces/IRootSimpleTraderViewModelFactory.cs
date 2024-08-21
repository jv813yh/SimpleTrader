using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.VVM.ViewModels.Factories.Interfaces
{
    public interface IRootSimpleTraderViewModelFactory
    {
        BaseViewModel CreateViewModel(ViewType viewType);
    }
}
