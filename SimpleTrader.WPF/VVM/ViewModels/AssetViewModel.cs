namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class AssetViewModel : BaseViewModel
    {
        public string Symbol { get; }
        public int Shares { get; }
        public AssetViewModel(string symbol, int shares)
        {
            Symbol = symbol;
            Shares = shares;
        }
    }
}
