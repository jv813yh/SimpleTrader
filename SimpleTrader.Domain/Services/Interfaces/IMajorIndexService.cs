using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.Interfaces
{
    public interface IMajorIndexService
    {
        //https://site.financialmodelingprep.com/developer/docs
        Task<MajorIndex> GetMajorIndexAsync(MajorIndexType majorIndexType);
    }
}
