using Newtonsoft.Json;

namespace SimpleTrader.FinancialModelingAPI.Results
{
    public class CompanyPrice
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
    }
}
