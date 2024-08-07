using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class MajorIndexViewModel
    {
        private readonly IMajorIndexService _majorIndexService;

        public MajorIndex DowJones { get; set; }
        public MajorIndex Nasdaq { get; set; }
        public MajorIndex SP500 { get; set; }

        public MajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }
        /// <summary>
        /// Factory method to create the MajorIndexViewModel and load the major indexes
        /// </summary>
        /// <param name="majorIndexService"></param>
        /// <returns></returns>
        public static MajorIndexViewModel CreateMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexViewModel majorIndexViewModel = new MajorIndexViewModel(majorIndexService);

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
