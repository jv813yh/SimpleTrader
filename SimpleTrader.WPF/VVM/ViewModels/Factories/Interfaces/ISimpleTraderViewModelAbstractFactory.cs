using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.VVM.ViewModels.Factories.Interfaces
{
    public interface ISimpleTraderViewModelAbstractFactory
    {
        BaseViewModel CreateViewModel(ViewType viewType);
    }
}
