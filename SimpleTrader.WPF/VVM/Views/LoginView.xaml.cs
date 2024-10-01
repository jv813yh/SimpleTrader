using Microsoft.IdentityModel.Tokens;
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
                // Check if the username and password are not empty
                if(!string.IsNullOrEmpty(pbPassword.Password) &&
                    !string.IsNullOrEmpty(txtBoxUserName.Text))
                {
                    LoginCommand.Execute(pbPassword.Password);
                }
            }
        }
    }
}
