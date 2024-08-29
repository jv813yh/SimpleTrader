using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public static readonly DependencyProperty LoginCommandProperty = 
            DependencyProperty.Register(nameof(LoginCommand), typeof(ICommand), typeof(LoginView), new PropertyMetadata(null));
        public ICommand LoginCommand
        {
            get { return (ICommand)GetValue(LoginCommandProperty); }
            set { SetValue(LoginCommandProperty, value); }
        }
        public LoginView()
        {
            InitializeComponent();
        }

        private void LoginClickHandler(object sender, RoutedEventArgs e)
        {
            if (LoginCommand != null)
            {
                if (LoginCommand.CanExecute(null))
                {
                    LoginCommand.Execute(pbPassword.Password);
                }
            }
        }
    }
}
