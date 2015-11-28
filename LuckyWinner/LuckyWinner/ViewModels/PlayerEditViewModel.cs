namespace Shared.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class PlayerEditViewModel : ViewModelBase
    {
        public PlayerEditViewModel()
        {
            Title = "Sorteo";
            Players = new ObservableCollection<PlayerViewModel>(new []
            {
                new PlayerViewModel { PlayerName = "Esteban Solano"}
            });
        }

        public ICollection<PlayerViewModel> Players { get; set; }

        private string _newPlayerName;

        public string NewPlayerName
        {
            get { return _newPlayerName; }
            set { _newPlayerName = value; }
        }

        public string Title { get; set; }

        private ICommand _playCommand;

        public ICommand PlayCommand
        {
            get { return _playCommand; }
            set { _playCommand = value; }
        }

        private PlayerViewModel _winner;
        public PlayerViewModel Winner
        {
            get { return _winner; }
            set
            {
                _winner = value;
                OnPropertyChanged("Winner");
            }
        }
    }
}