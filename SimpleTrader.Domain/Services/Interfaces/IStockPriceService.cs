namespace SimpleTrader.Domain.Services.Interfaces
{
    public interface IStockPriceService
    {
        // https://site.financialmodelingprep.com/developer/docs
        Task<double?> GetPriceAsync(string symbol);
    }
}
