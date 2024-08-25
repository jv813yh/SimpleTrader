namespace SimpleTrader.FinancialModelingAPI
{
    public class FinancialModelingHttpClientFactory
    {
        // API key for the financial modeling API
        private readonly string _apiKey;

        public FinancialModelingHttpClientFactory(string apiKey)
        {
            _apiKey = apiKey;
        }

        // Create a new instance of the FinancialModelingHttpClient
        public FinancialModelingHttpClient CreateHttpClient()
         => new FinancialModelingHttpClient(_apiKey);
    }
}
