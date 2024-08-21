using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.VVM.ViewModels.Factories.Interfaces;

namespace SimpleTrader.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : BaseCommand
    {
        private readonly INavigator _navigator;
        private readonly IRootSimpleTraderViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IRootSimpleTraderViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        /// <summary>
        /// Method to execute the command to update the current view model based on the parameter
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object? parameter)
        {
            if(parameter is ViewType viewType)
            {
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}
