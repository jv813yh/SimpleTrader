using SimpleTrader.Common.Models;

namespace SimpleTrader.Domain.Models
{
    public class AssetTransaction : CommonObject
    {
        public Account Account { get; set; }
        public bool IsPurchase { get; set; }
        public Asset Asset { get; set; }
        public int SharesAmount { get; set; }
        public DateTime DateProcessed { get; set; }
    }
}
