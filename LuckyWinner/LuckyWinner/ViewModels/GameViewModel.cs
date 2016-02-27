namespace Shared.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.Linq;
    using Xamarin.Forms;
	using System;

    public class GameViewModel : ViewModelBase
    {
        private readonly ObservableCollection<PlayerViewModel> _players;

        public ObservableCollection<PlayerViewModel> Participants { get; private set; }
        //public ObservableCollection<EventLogViewModel> History { get; private set; }
        public ObservableCollection<PlayerViewModel> Winners { get; private set; }

        public GameViewModel(GameService service)
        {
            Title = "Sorteo";
            Service = service;

            _players = new ObservableCollection<PlayerViewModel>();

			Model = new Game ();
			//History = new ObservableCollection<EventLogViewModel> ();
			Winners = new ObservableCollection<PlayerViewModel> ();
        }

        public Game Model { get; private set; }

		public void Load (Game model)
		{
			Model = model;
		}

        private void SetCommands(PlayerViewModel item)
        {
			item.DeleteCommand = new Command(() => RemovePlayer(item));
        }

		private async void RemovePlayer (PlayerViewModel item)
		{
			var index = _players.IndexOf (item);
			_players.Remove (item);

			Model.Participants.RemoveAt (index);
			await Service.SaveParticipantsAsync(Model);
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
                OnPropertyChanged();
            }
        }

        protected GameService Service { get; set; }

        private PlayerViewModel GetNewPlayer(string name)
        {
			Model.Participants.Add (name);
			var result = new PlayerViewModel(new User {Name = name});

            SetCommands(result);

            return result;
        }

        public async void AddPlayer(string fromText)
        {
            _players.Add(GetNewPlayer(fromText));

			await Service.SaveParticipantsAsync(Model);
        }

        private PlayerViewModel[] FromPlayerNames(string[] names)
        {
            return names.Select(item => new PlayerViewModel(new User {Name = item})).ToArray();
        }

        public async void Play()
        {
            if (Players.Any() == false)
            {
                return;
            }
            foreach (var item in Players)
            {
                item.IsWinner = false;
            }

            var random = new Random(DateTime.Now.Millisecond);

            var lucky = random.Next(0, Players.Count());
            var selectedPlayer = Players.ElementAtOrDefault(lucky);

            if (selectedPlayer != null)
            {
                selectedPlayer.IsWinner = true;
                Winner = selectedPlayer;

                //History.Add(new EventLogViewModel(string.Format("{0} winned!", selectedPlayer.PlayerName)));
				Model.History.Add(string.Format("{0} winned!", selectedPlayer.PlayerName));

				await Service.SaveHistoryAsync(Model);
            }
        }
    }
}