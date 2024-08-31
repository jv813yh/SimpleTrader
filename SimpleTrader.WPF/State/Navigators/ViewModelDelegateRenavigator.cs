using SimpleTrader.WPF.VVM.ViewModels;

namespace SimpleTrader.WPF.State.Navigators
{
    public class ViewModelDelegateRenavigator<TViewModel> : IRenavigator where TViewModel : BaseViewModel
    {
        // For state current view model
        private readonly INavigator _navigator;
        // For creating new view model according to the type
        private readonly CreateViewModel<TViewModel> _createViewModelDelegate;

        public ViewModelDelegateRenavigator(INavigator navigator, CreateViewModel<TViewModel> createViewModelDelegate)
        {
            _navigator = navigator;
            _createViewModelDelegate = createViewModelDelegate;
        }

        /// <summary>
        /// Method to renavigate the current view model according to the type of TViewModel where TViewModel : BaseViewModel
        /// </summary>
        public void Renavigate()
        {
            _navigator.CurrentViewModel = _createViewModelDelegate();
        }
    }
}
