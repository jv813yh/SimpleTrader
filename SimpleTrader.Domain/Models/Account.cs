using SimpleTrader.Common.Models;

namespace SimpleTrader.Domain.Models
{
    public class Account : CommonObject
    {
        public int idUser { get; set; }
        public double Balance { get; set; }
        public ICollection<AssetTransaction> AssetTransactions { get; set; }
    }
}
