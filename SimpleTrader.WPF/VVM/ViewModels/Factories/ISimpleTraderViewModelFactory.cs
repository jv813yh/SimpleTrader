using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.VVM.ViewModels.Factories
{
    public interface ISimpleTraderViewModelFactory
    {
        BaseViewModel CreateViewModel(ViewType viewType);
    }
}
