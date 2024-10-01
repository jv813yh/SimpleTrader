using System.ComponentModel;
using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public interface ISearchSymbolViewModel : INotifyPropertyChanged
    {
        double PricePerShare { set; }
        string SearchResultSymbol { set; }
        string SetErrorMessage { set; }
        string SetStatusMessage { set; }
        string SharesOwned { set;}
        string Symbol { get;  }
        bool CanSearchSymbol { get; }
    }
}