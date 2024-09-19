using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.Views
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : UserControl
    {
        public static readonly DependencyProperty SelectedAssetChangedCommandProperty =
            DependencyProperty.Register(nameof(SelectedAssetChangedCommand), typeof(ICommand), typeof(SellView), new PropertyMetadata(null));

        public ICommand SelectedAssetChangedCommand
        {
            get => (ICommand)GetValue(SelectedAssetChangedCommandProperty);
            set => SetValue(SelectedAssetChangedCommandProperty, value);
        }
        public SellView()
        {
            InitializeComponent();
        }

        private void cbAssets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbAssets.SelectedItem != null)
            {
                SelectedAssetChangedCommand?.Execute(null);
            }
        }
    }
}