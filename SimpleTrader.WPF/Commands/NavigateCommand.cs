using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.Commands
{
    public class NavigateCommand : BaseCommand
    {
        private readonly IRenavigator _renavigator;

        public NavigateCommand(IRenavigator renavigator)
        {
            _renavigator = renavigator;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            _renavigator.Renavigate();
        }
    }
}
