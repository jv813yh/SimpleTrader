using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.FinancialModelingAPI.Results;

namespace SimpleTrader.FinancialModelingAPI.Services
{
    public class StockPriceProvider : IStockPriceService
    {
        // URI to get the stock time
        private const string _stockRealTimeGlobal = "stock/real-time-price/";
        // FinancialModelingHttpClientFactory to create the HttpClient
        private readonly FinancialModelingHttpClient _financialModelingHttpClient;

        public StockPriceProvider(FinancialModelingHttpClient financialModelingHttpClient)
        {
            _financialModelingHttpClient = financialModelingHttpClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        /// <exception cref="InvalidSymbolException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<double> GetPriceAsync(string symbol)
        {
            double returnFirstPrice;
            // uri to get the StockPriceResult according to the symbol
            string fullUriToMajorIndex = _stockRealTimeGlobal + symbol;

            // Get the response message from the uri
            StockPriceResult stockResult = await _financialModelingHttpClient.GetAsync<StockPriceResult>(fullUriToMajorIndex);

            if (stockResult.CompaniesPriceList.Count == 0)
            {
                throw new InvalidSymbolException(symbol);
            }

            try
            {
                returnFirstPrice = stockResult.CompaniesPriceList
                    .Where(c => c.Symbol == symbol)
                    .First().Price;
            }
            catch (NullReferenceException)
            {
                throw new Exception("The price of the stock is not available");
            }
            catch (InvalidOperationException)
            {
                throw new Exception("The price of the stock is not available");
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

            // Return the price of stock
            return returnFirstPrice;
        }
    }
}
