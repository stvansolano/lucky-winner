using System;

namespace Shared.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.Linq;
    using Xamarin.Forms;

    public class GameViewModel : ViewModelBase
    {
        private readonly ObservableCollection<PlayerViewModel> _players;

        public ObservableCollection<PlayerViewModel> Participants { get; private set; }
        public ObservableCollection<EventLogViewModel> History { get; private set; }
        public ObservableCollection<PlayerViewModel> Winners { get; private set; }

        public GameViewModel(GameService service, Game model)
        {
            Title = "Sorteo";
            Service = service;
            Model = model;

            _players = new ObservableCollection<PlayerViewModel>();

            Load();
        }

        protected Game Model { get; set; }

        private void Load()
        {
        }

        public void FillPlayers()
        {
            _players.Clear();

            foreach (var player in Model.Participants)
            {
                var item = new PlayerViewModel(player);

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
                OnPropertyChanged();
            }
        }

        protected GameService Service { get; set; }

        private PlayerViewModel GetNewPlayer(string name)
        {
            var result = new PlayerViewModel(new User {Name = name});

            SetCommands(result);

            return result;
        }

        public void AddPlayer(string fromText)
        {
            _players.Add(GetNewPlayer(fromText));
        }

        private PlayerViewModel[] FromPlayerNames(string[] names)
        {
            return names.Select(item => new PlayerViewModel(new User {Name = item})).ToArray();
        }

        public void Play()
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

                History.Add(new EventLogViewModel(string.Format("{0} winned!", selectedPlayer.PlayerName)));
            }
        }
    }
}