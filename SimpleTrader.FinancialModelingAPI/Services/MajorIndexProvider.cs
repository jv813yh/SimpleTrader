using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces;

namespace SimpleTrader.FinancialModelingAPI.Services
{
    public class MajorIndexProvider : IMajorIndexService
    {
        // URI to get the major index
        private const string _majorIndexURIGlobal = "majors-indexes/";
        // FinancialModelingHttpClient to create the HttpClient
        private readonly FinancialModelingHttpClient _financialModelingHttpClient;
         
        public MajorIndexProvider(FinancialModelingHttpClient financialModelingHttpClient)
        {
            _financialModelingHttpClient = financialModelingHttpClient;
        }

        /// <summary>
        /// Async method to get the major index according to the majorIndexType
        /// </summary>
        /// <param name="majorIndexType"></param>
        /// <returns></returns>
        public async Task<MajorIndex> GetMajorIndexAsync(MajorIndexType majorIndexType)
        {
            // uri to get the major index according to the majorIndexType
            string fullUriToMajorIndex = _majorIndexURIGlobal + GetMajorIndexTypeSuffix(majorIndexType);

            // Get the response message from the uri
            MajorIndex? majorIndex = await _financialModelingHttpClient.GetAsync<MajorIndex>(fullUriToMajorIndex);

            if(majorIndex == null)
            {
                majorIndex = new MajorIndex();
            }

            // Set the major index type
            majorIndex.MajoxIndexType = majorIndexType;

            // Return the major index
            return majorIndex;
        }

        /// <summary>
        /// Method for creating the suffix for the major index type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string GetMajorIndexTypeSuffix(MajorIndexType type)
        {
            switch (type)
            {
                case MajorIndexType.DowJones:
                    return ".DJI";
                case MajorIndexType.Nasdaq:
                    return ".IXIC";
                case MajorIndexType.SP500:
                    return ".INX";
                default:
                    throw new NotImplementedException("MajorIndexType does not have a suffix defined");
            };
        }
    }
}
