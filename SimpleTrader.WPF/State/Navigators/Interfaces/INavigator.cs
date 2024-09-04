using SimpleTrader.WPF.VVM.ViewModels;
using System.Windows.Input;

namespace SimpleTrader.WPF.State.Navigators
{
    public enum ViewType
    {
        Home,
        Portfolio,
        Buy,
        Sell,
        Login
    }

    public interface INavigator
    {
        BaseViewModel CurrentViewModel { get; set; }
        event Action StateChanged; 
    }
}
