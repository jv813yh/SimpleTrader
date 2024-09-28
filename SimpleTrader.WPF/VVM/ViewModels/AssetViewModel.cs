namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class AssetViewModel : BaseViewModel
    {
        public string Symbol { get; init; } = string.Empty;
        public int Shares { get; init; } = 0;
        public double PricePerShare { get; init; } = 0;
        public double AssetValue 
            =>  Shares * PricePerShare;
        //public AssetViewModel(string symbol, int shares)
        //{
        //    Symbol = symbol;
        //    Shares = shares;
        //}
    }
}
