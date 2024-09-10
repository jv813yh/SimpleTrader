
namespace SimpleTrader.WPF.Commands
{
    public abstract class AsyncCommandBase : BaseCommand
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

        public override bool CanExecute(object? parameter)
         => !IsExecuting &&
            base.CanExecute(parameter);

        public override async void Execute(object? parameter)
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
