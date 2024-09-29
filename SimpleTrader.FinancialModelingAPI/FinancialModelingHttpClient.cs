using Newtonsoft.Json;
using SimpleTrader.FinancialModelingAPI.Models;
using System.Net;

namespace SimpleTrader.FinancialModelingAPI
{
    public class FinancialModelingHttpClient 
    {
        // API key for the financial modeling API
        private readonly string _apiKey;
        // HTTP client to make the requests
        private readonly HttpClient _httpClient;

        public FinancialModelingHttpClient(HttpClient httpClient, FinancialModelingAPIKey apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey.Key;
        }
        /// <summary>
        /// Async base method to get the response message from the uri
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                // Get the response message from the uri
                HttpResponseMessage responseMessage = await _httpClient.GetAsync(uri + $"?apikey={_apiKey}");

                // Get the raw content from the response message
                string rawContentJson = await responseMessage.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(rawContentJson))
                {
                    throw new Exception("Empty response from API");
                }

                // Deserialize the raw content to T object and return it
                return JsonConvert.DeserializeObject<T>(rawContentJson);
            }
            catch (WebException webEx)
            {
               using(var respone = webEx.Response)
               {
                    using(var data = respone.GetResponseStream())
                    {
                        string text = new StreamReader(data).ReadToEnd();
                        throw new Exception(text);
                    }
               }
            }
            catch (HttpRequestException httpEx)
            {
                // Handle HTTP specific errors
                throw new Exception($"Request error: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get data from the API: {ex}");
            }
        }
    }
}
