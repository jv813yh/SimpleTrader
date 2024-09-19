using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public interface ISearchSymbolViewModel
    {
        double PricePerShare { set; }
        string SearchResultSymbol { set; }
        string SetErrorMessage { set; }
        string Symbol { get;  }
    }
}