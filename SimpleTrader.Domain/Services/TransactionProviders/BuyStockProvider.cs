using SimpleTrader.Common.Interfaces;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.Domain.Services.Interfaces.TransactionServices;

namespace SimpleTrader.Domain.Services.TransactionProviders
{
    public class BuyStockProvider : IBuyStockService
    {
        private readonly IStockPriceService _stockPriceProvider;
        private readonly ICommonRepository<Account> _dataRepository;

        public BuyStockProvider(IStockPriceService stockPriceService, ICommonRepository<Account> dataService)
        {
            _stockPriceProvider = stockPriceService;
            _dataRepository = dataService;
        }

        /// <summary>
        /// Async method to buy a stock for a given account
        /// </summary>
        /// <param name="buyer"></param>
        /// <param name="stockSymbol"></param>
        /// <param name="shares"></param>
        /// <returns></returns>
        public async Task<Account> BuyStockAsync(Account buyer, string stockSymbol, int shares)
        {
            // Check if the buyer is null
            if(buyer == null)
            {
                throw new ArgumentNullException(nameof(buyer), "Buyer is not set");
            }

            // Check if the stock symbol is null or empty
            if(string.IsNullOrEmpty(stockSymbol))
            {
                throw new ArgumentNullException(nameof(stockSymbol),"Stock symbol is not set");
            }

            // Check if the shares are less than or equal to 0
            if(shares <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(shares), "Shares must be greater than 0");
            }


            // Get the current price of the stock
            double currentStockPrice = await _stockPriceProvider.GetPriceAsync(stockSymbol);

            // Calculate the new balance of the buyer
            double resultBalance = buyer.Balance - (currentStockPrice * shares);

            // Check if the buyer has enough balance to buy the stock
            if(resultBalance >= 0)
            {
                buyer.Balance = resultBalance;
            }
            else
            {
                throw new InsufficientBalanceOfMoney(resultBalance, "Result balance has to be more than 0");
            }

            // Create a new AssetTransaction
            AssetTransaction newAssetTransaction = new AssetTransaction()
            {
                Account = buyer,
                
                Asset = new Asset()
                {
                    Symbol = stockSymbol,
                    PricePerShares = currentStockPrice
                },
                IsPurchase = true,
                SharesAmount = shares,
                DateProcessed = DateTime.Now
            };

            // Update the account balance
            buyer.AssetTransactions.Add(newAssetTransaction);

            // Create the new asset transaction in the database
            await _dataRepository.UpdateAsync(buyer.Id, buyer);

            // Return the buyer
            return buyer;
        }
    }
}
