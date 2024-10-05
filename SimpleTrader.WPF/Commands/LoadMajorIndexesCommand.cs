using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.WPF.VVM.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class LoadMajorIndexesCommand : AsyncCommandBase
    {
        private readonly MajorIndexListingViewModel _majorIndexListingViewModel;
        private readonly IMajorIndexService _majorIndexService;

        public LoadMajorIndexesCommand(MajorIndexListingViewModel majorIndexListingViewModel, IMajorIndexService majorIndexService)
        {
            _majorIndexListingViewModel = majorIndexListingViewModel;
            _majorIndexService = majorIndexService;
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            _majorIndexListingViewModel.IsLoading = true;

            await Task.WhenAll(LoadDowJones(),
                         LoadNasdaq(),
                         LoadSP500());

            _majorIndexListingViewModel.IsLoading = false;
        }


        private async Task LoadDowJones()
        {
            _majorIndexListingViewModel.DowJones = await _majorIndexService.GetMajorIndexAsync(MajorIndexType.DowJones);
        }

        private async Task LoadNasdaq()
        {
            _majorIndexListingViewModel.Nasdaq = await _majorIndexService.GetMajorIndexAsync(MajorIndexType.Nasdaq);
        }

        private async Task LoadSP500()
        {
            _majorIndexListingViewModel.SP500 = await _majorIndexService.GetMajorIndexAsync(MajorIndexType.SP500);
        }
    }
}
