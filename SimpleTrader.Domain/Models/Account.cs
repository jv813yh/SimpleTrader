using SimpleTrader.Common.Models;

namespace SimpleTrader.Domain.Models
{
    public class Account : CommonObject
    {
        public User AccountHolder { get; set; }
        public double Balance { get; set; }
        public ICollection<AssetTransaction> AssetTransactions { get; set; }
    }
}
