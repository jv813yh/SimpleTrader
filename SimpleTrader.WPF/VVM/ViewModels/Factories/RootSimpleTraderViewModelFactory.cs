using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.VVM.ViewModels.Factories.Interfaces;

namespace SimpleTrader.WPF.VVM.ViewModels.Factories
{
    public class RootSimpleTraderViewModelAbstractFactory : IRootSimpleTraderViewModelFactory
    {
        private readonly ISimpleTraderViewModelFactory<HomeViewModel> _homeViewModelFactory;
        private readonly ISimpleTraderViewModelFactory<PortfolioViewModel> _portfolioViewModelFactory;
        private readonly BuyViewModel _buyViewModel;

        public RootSimpleTraderViewModelAbstractFactory(ISimpleTraderViewModelFactory<HomeViewModel> homeViewModelFactory, 
            ISimpleTraderViewModelFactory<PortfolioViewModel> portfolioViewModelFactory,
            BuyViewModel buyViewModel)
        {
            _homeViewModelFactory = homeViewModelFactory;
            _portfolioViewModelFactory = portfolioViewModelFactory;
            _buyViewModel = buyViewModel;
        }

        /// <summary>
        /// Method to create a ViewModel based on the ViewType
        /// </summary>
        /// <param name="viewType"> ViewType </param>
        /// <returns> Cuurent ViewModel </returns>
        /// <exception cref="Exception"></exception>
        public BaseViewModel CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return _homeViewModelFactory.CreateViewModel();
                case ViewType.Portfolio:
                    return _portfolioViewModelFactory.CreateViewModel();
                case ViewType.Buy:
                    return _buyViewModel;
                default:
                    throw new Exception("View type does not have a ViewModel in the current version.");
            }
        }
    }
}
