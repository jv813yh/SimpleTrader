using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.FinancialModelingAPI.Results;

namespace SimpleTrader.FinancialModelingAPI.Services
{
    public class StockPriceProvider : IStockPriceService
    {
        // URI to get the stock time
        private const string _stockRealTimeGlobal = "stock/real-time-price/";
        private const string _apiKey = "3caTRYHV1GznUExhsAqG9h8NeRgXY1rN";

        public async Task<double> GetPriceAsync(string symbol)
        {
            // uri to get the StockPriceResult according to the symbol
            string fullUriToMajorIndex = _stockRealTimeGlobal + symbol + $"?apikey={_apiKey}";

            using (FinancialModelingHttpClient client = new FinancialModelingHttpClient())
            {
                // Get the response message from the uri
                StockPriceResult stockResult = await client.GetAsync<StockPriceResult>(fullUriToMajorIndex);

                if (stockResult.CompaniesPriceList.Count == 0)
                {
                    throw new InvalidSymbolException(symbol);
                }

                double firstPrice = stockResult.CompaniesPriceList
                    .Where(c => c.Symbol == symbol)
                    .FirstOrDefault().Price;

                // Return the price of stock
                return firstPrice;
            }
        }
    }
}
