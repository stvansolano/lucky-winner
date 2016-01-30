using Xamarin.Forms;

namespace Shared.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.Linq;

    public class GameViewModel : ViewModelBase
    {
        public GameViewModel(NetworkService network)
        {
            Title = "Sorteo";
            Network = network;

            _players = new ObservableCollection<PlayerViewModel>();
            Groups = new ObservableCollection<GroupViewModel>(
                new[]
                {
                    GetLocalGroup(),
                    GetMeetupGroup()
                }
            );

            SelectedGroup = Groups.First();
        }

        private void FillPlayers()
        {
            _players.Clear();

            if (SelectedGroup == null)
            {
                return;
            }
            foreach (var player in SelectedGroup.Contacts)
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

        private GroupViewModel _selectedGroup;
        private ObservableCollection<PlayerViewModel> _players;

        public GroupViewModel SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;

                FillPlayers();
                OnPropertyChanged();
                OnPropertyChanged("SelectedGroupTitle");
            }
        }

        public string SelectedGroupTitle
        {
            get { return SelectedGroup == null ? string.Empty : SelectedGroup.GroupName; }
        }

        public NetworkService Network { get; set; }

        public IEnumerable<GroupViewModel> Groups { get; set; }

        public IList<string> GroupNames
        {
            get { return Groups.Select(item => item.GroupName).ToList(); }
        }

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

        private GroupViewModel GetLocalGroup()
        {
            return new GroupViewModel
            {
                GroupName = "Local",
				Contacts = FromPlayerNames(new [] {
					"Juan Carlos",
					"Jairo Esquivel",
					"Stuart Sanchez",
					"Esteban",
					"Kimberly",
					"Heizel Martínez Garro",
					"Elián Acuña Fernández"
				})
            };
        }

		private ContactViewModel[] FromPlayerNames(string[] names) {
			return names.Select (item => new ContactViewModel { Name = item}).ToArray();
		}

        private GroupViewModel GetMeetupGroup()
        {
            return new GroupViewModel
            {
                GroupName = "Meetup",
				Contacts = FromPlayerNames(new [] {
					"Orlando Sanchez",
					"Jairo Esquivel",
					"Marvin Solano",
					"Carlos Mendez",
					"David",
					"Fernando Valverde Chavarría",
					"Steven",
					"Aaron Cyrman",
					"Eduardo Fonseca",
					"Daniel Lacayo",
					"Cesar Guillen Oreamuno",
					"Ricardo Jimenez Guido",
					"Oscar Ulloa Retana",
					"Anthony Martinez",
					"Guillermo Loaiza",
					"Jia Ming Liou",
					"Daniel Araya",
					"Stuart Sanchez",
					"Kenneth Barquero"
				})
            };
        }
    }
}