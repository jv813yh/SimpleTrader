namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        private string _messsage = string.Empty;
        public string Message
        {
            get => _messsage;
            set
            {
                _messsage = value;
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
            }
        }

        public bool HasMessage
            => !string.IsNullOrEmpty(Message);

    }
}
