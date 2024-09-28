using SimpleTrader.Domain.Exceptions;
using SimpleTrader.WPF.State.Assets;
using SimpleTrader.WPF.VVM.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class SetColumnSeriesCommand : AsyncCommandBase
    {
        private readonly PortfolioViewModel _portfolioViewModel;
        private readonly AssetStore _assetStore;

        public SetColumnSeriesCommand(PortfolioViewModel portfolioViewModel, AssetStore assetStore)
        {
            _portfolioViewModel = portfolioViewModel;
            _assetStore = assetStore;

        }

        public override async Task ExecuteAsync(object? parameter)
        {
            // Clear the message for the x and y axis
            _portfolioViewModel.MessageForXAxis = string.Empty;
            _portfolioViewModel.MessageForYAxis = string.Empty;

            // Clear the collection for the chart
            _portfolioViewModel.CollectionForChartWithNames.Clear();
            _portfolioViewModel.CollectionForChartWithAmounts.Clear();


            try
            {
                if (_portfolioViewModel.IsAccordingAmountOfAssets)
                {
                    _portfolioViewModel.MessageForXAxis = "Name of Assets";
                    _portfolioViewModel.MessageForYAxis = "Amount of Assets";

                    IEnumerable<AssetViewModel> amountOfAssets = _assetStore.GetAssetsOrderByDescending(_portfolioViewModel.SelectedOption.Number);

                    // Set data for the chart
                    SetDataForChart(amountOfAssets, true);

                    // Set chart visibility
                    SetChartVisibility();
                }
                else
                {
                    _portfolioViewModel.MessageForXAxis = "Name of Assets";
                    _portfolioViewModel.MessageForYAxis = "Amount of Money";

                    // Get assets according to the money
                    IEnumerable<AssetViewModel> amountOfMoney = await _assetStore.GetAssetsAccordingByMoneyByDescending(_portfolioViewModel.SelectedOption.Number);

                    // Set data for the chart
                    SetDataForChart(amountOfMoney, false);

                    // Set chart visibility
                    SetChartVisibility();
                }
            }
            catch (InvalidSymbolException)
            {
                _portfolioViewModel.SetErrorMessage = "Problem with loading the symbol";
            }
            catch (Exception)
            {
               _portfolioViewModel.SetErrorMessage = "Problem with loading data into the graph";
            }
        }

        /// <summary>
        /// Set data for the chart
        /// </summary>
        /// <param name="assets"></param>
        /// <param name="isAsset"></param>
        private void SetDataForChart(IEnumerable<AssetViewModel> assets, bool isAsset)
        {
            foreach (var asset in assets)
            {
                _portfolioViewModel.CollectionForChartWithNames.Add(asset.Symbol);
                _portfolioViewModel.CollectionForChartWithAmounts.Add(isAsset ? asset.Shares : Math.Round(asset.AssetValue, 2));
            }
        }

        /// <summary>
        /// Set chart visibility
        /// </summary>
        private void SetChartVisibility()
        {
            _portfolioViewModel.IsChartVisible = _portfolioViewModel.CollectionForChartWithNames.Any();
        }
    }
}
