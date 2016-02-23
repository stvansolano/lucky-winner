namespace Shared.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.Linq;
	using Xamarin.Forms;

    public class GameViewModel : ViewModelBase
    {
        public GameViewModel(NetworkService network)
        {
            Title = "Sorteo";
            Network = network;

            _players = new ObservableCollection<PlayerViewModel>();
        }

        public void FillPlayers(GroupViewModel group)
        {
            _players.Clear();

            foreach (var player in group.Contacts)
            {
                var item = new PlayerViewModel {PlayerName = player.Name};

                SetCommands(item);

                _players.Add(item);
            }
        }

        private void SetCommands(PlayerViewModel item)
        {
            item.DeleteCommand = new Command(() => _players.Remove(item));
        }

        public IEnumerable<PlayerViewModel> Players
        {
            get { return _players; }
        }

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

        private ObservableCollection<PlayerViewModel> _players;

        protected NetworkService Network { get; set; }

        private PlayerViewModel GetNewPlayer(string text)
        {
            var result = new PlayerViewModel { PlayerName = text };

            SetCommands(result);

            return result;
        }

        public void AddPlayer(string fromText)
        {
            _players.Add(GetNewPlayer(fromText));
        }

		private ContactViewModel[] FromPlayerNames(string[] names) {
			return names.Select (item => new ContactViewModel { Name = item}).ToArray();
		}
    }
}