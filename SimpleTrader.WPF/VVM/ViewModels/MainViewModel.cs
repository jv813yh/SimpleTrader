using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class MainViewModel 
    {
        public INavigator Navigator { get; set; } = new Navigator();

        public MainViewModel()
        {
            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Home);
        }
    }
}
