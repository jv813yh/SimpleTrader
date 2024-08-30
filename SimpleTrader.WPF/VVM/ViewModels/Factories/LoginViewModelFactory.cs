using SimpleTrader.WPF.State.Authentificators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.VVM.ViewModels.Factories.Interfaces;

namespace SimpleTrader.WPF.VVM.ViewModels.Factories
{
    public class LoginViewModelFactory : ISimpleTraderViewModelFactory<LoginViewModel>
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LoginViewModelFactory(IAuthenticator authenticator, 
            IRenavigator renavigator)
        {
            _authenticator = authenticator;
            _renavigator = renavigator;

        }

        public LoginViewModel CreateViewModel()
         => new LoginViewModel(_authenticator, _renavigator);
    }
}
