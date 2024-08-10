using Newtonsoft.Json;

namespace SimpleTrader.FinancialModelingAPI.Results
{
    public class StockPriceResult
    {
        [JsonProperty("companiesPriceList")]
        public List<CompanyPrice> CompaniesPriceList { get; set; }
    }

}
