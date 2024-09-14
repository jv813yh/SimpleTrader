using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces;
using SimpleTrader.Domain.Services.Interfaces.TransactionServices;

namespace SimpleTrader.Domain.Services.TransactionProviders
{
    public class SellStockProvider : ISellStockService
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IAccountService _accountService;

        public SellStockProvider(IAccountService accountService, IStockPriceService stockPriceService)
        {
            _accountService = accountService;
            _stockPriceService = stockPriceService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="seller"></param>
        /// <param name="symbol"></param>
        /// <param name="shares"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InsufficientSharesException"></exception>
        public async Task<Account> SellStockAsync(Account seller, string symbol, int sharesForSell)
        {
            // Validate the parameters
            if(seller == null)
            {
                throw new NullReferenceException("Seller is not set");
            }

            if(string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException("Symbol is not set");
            }

            // Validate seller has sufficient shares to sell
            int sharesOwned = GetAccountSharesForSymbol(seller, symbol);
            if (sharesOwned < sharesForSell)
            {
                throw new InsufficientSharesException(symbol, sharesOwned, sharesForSell);
            }

            // Get price of the stock
            double currentStockPrice = await _stockPriceService.GetPriceAsync(symbol);

            // Add the transaction to the account
            seller.AssetTransactions.Add(new AssetTransaction
            {
                Account = seller,
                Asset = new Asset
                {
                    Symbol = symbol,
                    PricePerShares = currentStockPrice
                },
                IsPurchase = false,
                SharesAmount = sharesForSell,
                DateProcessed = DateTime.Now
            });

            // Add the new balance
            seller.Balance += currentStockPrice * sharesForSell;

            // Update the account with the new transaction
            Account? resultUpdatedAccount = await _accountService.UpdateAsync(seller.Id, seller);
            if(resultUpdatedAccount == null)
            {
                throw new Exception("The transaction failed");
            }

            return resultUpdatedAccount;
        }

        private int GetAccountSharesForSymbol(Account seller, string symbol)
        {
            return seller.AssetTransactions.Where(t => t.Asset.Symbol == symbol)
                                           .Sum(a => a.IsPurchase ? a.SharesAmount : -a.SharesAmount);    
        }
    }
}
