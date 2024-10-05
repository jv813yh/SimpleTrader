using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.WPF.Commands;
using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class MajorIndexListingViewModel : BaseViewModel
    {
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

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public ICommand LoadMajorIndexesCommand { get; }
        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            LoadMajorIndexesCommand = new LoadMajorIndexesCommand(this, majorIndexService);
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
            majorIndexViewModel.LoadMajorIndexesCommand.Execute(null);

            return majorIndexViewModel;
        }
    }
}
