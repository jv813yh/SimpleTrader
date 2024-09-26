using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.Views
{
    /// <summary>
    /// Interaction logic for PortfolioView.xaml
    /// </summary>
    public partial class PortfolioView : UserControl
    {
        public static readonly DependencyProperty SelectedTopChangedCommandProperty = 
            DependencyProperty.Register(nameof(SelectedTopChangedCommand), typeof(ICommand), typeof(PortfolioView), new PropertyMetadata(null));

        public ICommand SelectedTopChangedCommand
        {
            get { return (ICommand)GetValue(SelectedTopChangedCommandProperty); }
            set { SetValue(SelectedTopChangedCommandProperty, value); }
        }
        public PortfolioView()
        {
            InitializeComponent();
        }

        private void cbTop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbTop.SelectedItem != null)
            {
                SelectedTopChangedCommand?.Execute(null);
            }
        }
    }
}
