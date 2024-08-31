using System.ComponentModel;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    // Base delegate to create a ViewModel according to the type
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : BaseViewModel;
    public class BaseViewModel : ObservableObject
    {

    }
}
