namespace Shared.ViewModels
{
    using System.Windows.Input;

    public class PlayerViewModel : ViewModelBase
    {
		public PlayerViewModel(User model)
        {
            PlayerName = model.Name;
			Model = model;
        }

		public User Model { get; set; }

        private string _playerName;
        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                OnPropertyChanged();
            }
        }

        private ICommand _deleteCommand;
        private bool _isWinner;

        public ICommand DeleteCommand
        {
            get { return _deleteCommand; }
            set { _deleteCommand = value; }
        }

        public bool IsWinner
        {
            get { return _isWinner; }
            set
            {
                _isWinner = value;
                OnPropertyChanged();
            }
        }
    }
}