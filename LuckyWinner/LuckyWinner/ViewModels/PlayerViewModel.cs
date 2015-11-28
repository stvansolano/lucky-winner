namespace Shared.ViewModels
{
    using System.Windows.Input;

    public class PlayerViewModel : ViewModelBase
    {
        private string _playerName;
        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                OnPropertyChanged("PlayerName");
            }
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get { return _deleteCommand; }
            set { _deleteCommand = value; }
        }
    }
}