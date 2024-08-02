using SimpleTrader.WPF.VVM.ViewModels;
using System.Windows;

namespace SimpleTrader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            // Create the startup window
            Window window = new MainWindow()
            {
                DataContext = new MainViewModel()
            };

            window.Show();

            base.OnStartup(e);
        }
    }

}
