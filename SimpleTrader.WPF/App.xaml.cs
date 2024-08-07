using SimpleTrader.Domain.Models;
using SimpleTrader.FinancialModelingAPI.Services;
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
                // Set the data context of the window to the MainViewModel
                DataContext = new MainViewModel()
            };

            window.Show();

            base.OnStartup(e);
        }
    }

}
