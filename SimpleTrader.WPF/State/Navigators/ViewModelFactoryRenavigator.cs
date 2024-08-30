using SimpleTrader.WPF.VVM.ViewModels;
using SimpleTrader.WPF.VVM.ViewModels.Factories.Interfaces;

namespace SimpleTrader.WPF.State.Navigators
{
    public class ViewModelFactoryRenavigator<TViewModel> : IRenavigator where TViewModel : BaseViewModel
    {
        // For state current view model
        private readonly INavigator _navigator;
        // For creating new view model according to the type
        private readonly ISimpleTraderViewModelFactory<TViewModel> _viewModelFactory;

        public ViewModelFactoryRenavigator(INavigator navigator, ISimpleTraderViewModelFactory<TViewModel> viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        /// <summary>
        /// Method to renavigate the current view model according to the type of TViewModel where TViewModel : BaseViewModel
        /// </summary>
        public void Renavigate()
        {
            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel();
        }
    }
}
