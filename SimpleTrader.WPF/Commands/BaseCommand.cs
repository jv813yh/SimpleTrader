using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public void OnRaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public virtual bool CanExecute(object? parameter)
            => true;

        public abstract void Execute(object? parameter);
    }
}
