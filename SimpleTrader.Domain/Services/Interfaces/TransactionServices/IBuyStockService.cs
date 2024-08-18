using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.Interfaces.TransactionServices
{
    public interface IBuyStockService
    {
        Task<Account> BuyStockAsync(Account buyer, string stockSymbol, int shares);
    }
}
