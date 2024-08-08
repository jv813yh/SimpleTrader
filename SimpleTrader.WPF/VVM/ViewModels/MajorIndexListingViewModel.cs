using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class MajorIndexListingViewModel : BaseViewModel
    {
        private readonly IMajorIndexService _majorIndexService;

        private MajorIndex _dowJones;
        private MajorIndex _nasdaq;
        private MajorIndex _sP500;
        public MajorIndex DowJones 
        { 
            get => _dowJones;
            set
            {
                _dowJones = value;
                OnPropertyChanged(nameof(DowJones));
            }
        }
        public MajorIndex Nasdaq 
        { 
            get => _nasdaq;
            set
            {
                _nasdaq = value;
                OnPropertyChanged(nameof(Nasdaq));
            }
        }
        public MajorIndex SP500 
        {
            get => _sP500;
            set
            {
                _sP500 = value;
                OnPropertyChanged(nameof(SP500));
            }
        }

        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }
        /// <summary>
        /// Factory method to create the MajorIndexViewModel and load the major indexes
        /// </summary>
        /// <param name="majorIndexService"></param>
        /// <returns></returns>
        public static MajorIndexListingViewModel CreateMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexListingViewModel majorIndexViewModel = new MajorIndexListingViewModel(majorIndexService);

            // Load the major indexes 
            majorIndexViewModel.LoadMajorIndexex();

            return majorIndexViewModel;
        }

        /// <summary>
        /// Method to load the major indexes according to the MajorIndexType with async non-blocking
        /// </summary>
        /// <returns></returns>
        private void LoadMajorIndexex()
        {
            _majorIndexService.GetMajorIndexAsync(MajorIndexType.DowJones).ContinueWith(task =>
            {
                if(task.Exception == null &&
                    task.Result != null)
                {
                    DowJones = task.Result;
                }
            });

            _majorIndexService.GetMajorIndexAsync(MajorIndexType.Nasdaq).ContinueWith(task =>
            {
                if (task.Exception == null &&
                    task.Result != null)
                {
                    Nasdaq = task.Result;
                }
            });

            _majorIndexService.GetMajorIndexAsync(MajorIndexType.SP500).ContinueWith(task =>
            {
                if (task.Exception == null &&
                    task.Result != null)
                {
                    SP500  = task.Result;
                }
            });
        }
    }
}
