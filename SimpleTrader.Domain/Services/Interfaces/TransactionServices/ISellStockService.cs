using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.Domain.Services.Interfaces.TransactionServices
{
    public interface ISellStockService
    {
        /// <summary>
        /// Sell a stock from the seller's account.
        /// </summary>
        /// <param name="seller"> The account of the seller. </param>
        /// <param name="symbol"> The symbol sold. </param>
        /// <param name="shares"> The amount of shares to sell. </param>
        /// <returns> The udpated account. </returns>
        /// <exception cref="InvalidSymbolException"> Thrown if the purchased symbol is invalid. </exception>
        /// <exception cref="InsufficientSharesException"> Thrown if the shares are insufficient </exception>
        /// <exception cref="NullReferenceException"> Thrown if we use not reference objects </exception>
        /// <exception cref="Exception"> Thrown if the trancasction fails. </exception>
        Task<Account> SellStockAsync(Account seller, string symbol, int shares);
    }
}
