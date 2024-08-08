using SimpleTrader.FinancialModelingAPI.Services;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.VVM.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : BaseCommand
    {
        private readonly INavigator _navigator;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        /// <summary>
        /// Method to execute the command to update the current view model based on the parameter
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object? parameter)
        {
            if(parameter is ViewType viewType)
            {
                switch(viewType)
                {
                    case ViewType.Home:
                        _navigator.CurrentViewModel = new HomeViewModel(MajorIndexListingViewModel.CreateMajorIndexViewModel(new MajorIndexProvider()));
                        break;
                    case ViewType.Portfolio:
                        _navigator.CurrentViewModel = new PortfolioViewModel();
                        break;
                    default:
                        _navigator.CurrentViewModel = new HomeViewModel(MajorIndexListingViewModel.CreateMajorIndexViewModel(new MajorIndexProvider()));
                        break;
                }
            }
        }
    }
}
