using SimpleTrader.WPF.State.Assets;
using SimpleTrader.WPF.VVM.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class SetColumnSeriesCommand : BaseCommand
    {
        private readonly PortfolioViewModel _portfolioViewModel;
        private readonly AssetStore _assetStore;

        public SetColumnSeriesCommand(PortfolioViewModel portfolioViewModel, AssetStore assetStore)
        {
            _portfolioViewModel = portfolioViewModel;
            _assetStore = assetStore;

        }

        public override void Execute(object? parameter)
        {
            _portfolioViewModel.MessageForXAxis = string.Empty;
            _portfolioViewModel.MessageForYAxis = string.Empty;

            if (_portfolioViewModel.IsAccordingAmountOfAssets)
            {
                _portfolioViewModel.MessageForXAxis = "Name of Assets";
                _portfolioViewModel.MessageForYAxis = "Amount of Assets";

                IEnumerable<AssetViewModel> amountOfAssets = _assetStore.GetAssetsOrderByDescending(_portfolioViewModel.SelectedOption.Number);

                _portfolioViewModel.CollectionForChartWithNames.Clear();
                _portfolioViewModel.CollectionForChartWithAmounts.Clear();

                foreach (var asset in amountOfAssets)
                {
                    _portfolioViewModel.CollectionForChartWithNames.Add(asset.Symbol);
                    _portfolioViewModel.CollectionForChartWithAmounts.Add(asset.Shares);
                }

                _portfolioViewModel.IsChartVisible = _portfolioViewModel.CollectionForChartWithNames.Any();
            }
            else
            {
                _portfolioViewModel.MessageForXAxis = "Name of Assets";
                _portfolioViewModel.MessageForYAxis = "Amount of Money";

                // GetAssetsAccordngByMoneyByDescending()

                _portfolioViewModel.CollectionForChartWithNames.Clear();
                _portfolioViewModel.CollectionForChartWithAmounts.Clear();

                // foreach (var asset in amountOfAssets)

                _portfolioViewModel.IsChartVisible = _portfolioViewModel.CollectionForChartWithNames.Any();
            }
        }
    }
}
