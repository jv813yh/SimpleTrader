
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public abstract class AsyncCommandBase : ICommand
    {
        public bool _isExecuting;
        public bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                OnRaiseCanExecuteChanged();
            }
        }

        public event EventHandler? CanExecuteChanged;

        public void OnRaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, System.EventArgs.Empty);
        }

        public virtual bool CanExecute(object? parameter)
        {
            return !IsExecuting;
        }

        public async void Execute(object? parameter)
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception)
            {

                throw;
            }

            IsExecuting = false;
        }

        public abstract Task ExecuteAsync(object? parameter);
    }
}
