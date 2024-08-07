
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces;
using System.Text.Json;

namespace SimpleTrader.FinancialModelingAPI.Services
{
    public class MajorIndexProvider : IMajorIndexService
    {
        // URI to get the major index
        private const string _majorIndexURIGlobal = "https://financialmodelingprep.com//api/v3/majors-indexes/";

        private const string _apiKey = "3caTRYHV1GznUExhsAqG9h8NeRgXY1rN";

        /// <summary>
        /// Async method to get the major index according to the majorIndexType
        /// </summary>
        /// <param name="majorIndexType"></param>
        /// <returns></returns>
        public async Task<MajorIndex?> GetMajorIndexAsync(MajorIndexType majorIndexType)
        {
            // uri to get the major index according to the majorIndexType
            string fullUriToMajorIndex = _majorIndexURIGlobal + GetMajorIndexTypeSuffix(majorIndexType) + $"?apikey={_apiKey}";


            using (HttpClient client = new HttpClient())
            {
                // Get the response message from the uri
                HttpResponseMessage responseMessage = await client.GetAsync(fullUriToMajorIndex);

                // Get the raw content from the response message
                string rawContentJson = await  responseMessage.Content.ReadAsStringAsync();

                // Deserialize the raw content to MajorIndex
                MajorIndex? majorIndex = JsonSerializer.Deserialize<MajorIndex>(rawContentJson);

                if(majorIndex != null)
                {
                    // Set the major index type
                    majorIndex.MajoxIndexType = majorIndexType;
                }

                // Return the major index
                return majorIndex;
            }
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
                    throw new NotImplementedException("Major index type not implemented");

            };
        }
    }
}
