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
                Contacts = new[]
                {
                    new ContactViewModel {Name = "Juan Carlos"},
                    new ContactViewModel {Name = "Jairo Esquivel"},
                    new ContactViewModel {Name = "Stuart Sanchez"},
                    new ContactViewModel {Name = "Esteban"},
                    new ContactViewModel {Name = "Kimberly"},
                    new ContactViewModel {Name = "Heizel Martínez Garro"},
                    new ContactViewModel {Name = "Elián Acuña Fernández"},
                }
            };
        }

        private GroupViewModel GetMeetupGroup()
        {
            return new GroupViewModel
            {
                GroupName = "Meetup",
                Contacts = new[]
                {
                    new ContactViewModel {Name = "Carlos Argüello"},
                    new ContactViewModel {Name = "Marco Antonio Gómez"},
                    new ContactViewModel {Name = "Daniel Lacayo"},
                    new ContactViewModel {Name = "Carlos Cervantes Gutierrez"},
                    new ContactViewModel {Name = "Geraldin Torres"},
                    new ContactViewModel {Name = "Xiannie Hylton"},
                    new ContactViewModel {Name = "May Valverde"},
                    new ContactViewModel {Name = "Richard Fernández"},
                    new ContactViewModel {Name = "Herber Madrigal"},
                    new ContactViewModel {Name = "Oscar Arroyo Fuentes"},
                    new ContactViewModel {Name = "Aaron Cedeño Martí­nez"},
                    new ContactViewModel {Name = "Andrés Carvajal"},

                    new ContactViewModel {Name = "Andrey Alfaro"},
                    new ContactViewModel {Name = "JC"},
                    new ContactViewModel {Name = "Alejandra Herrera P"},
                    new ContactViewModel {Name = "Cesar Guillen Oreamuno"},
                    new ContactViewModel {Name = "Jairo Esquivel"},
                    new ContactViewModel {Name = "Stuart Sanchez"},
                    new ContactViewModel {Name = "Kenneth P. Barquero"},
                    new ContactViewModel {Name = "Kimberly"},
                    new ContactViewModel {Name = "Heizel Martínez Garro"},
                    new ContactViewModel {Name = "Elián Acuña Fernández"},
                    new ContactViewModel {Name = "Esteban (estsantre96@hotmail.com)"},

                    new ContactViewModel {Name = "Deibyd"},
                    new ContactViewModel {Name = "Esteban Hernandez"},
                    new ContactViewModel {Name = "Javier Nuñez"},
                    new ContactViewModel {Name = "Jose Alberto Gonzalez Sanchez"},
                    new ContactViewModel {Name = "Keneth Murillo"},
                    new ContactViewModel {Name = "Pablo Duran"},
                    new ContactViewModel {Name = "Pablo Salas"},
                    new ContactViewModel {Name = "Roberto Chacón"},
                    new ContactViewModel {Name = "Steven"},
                    new ContactViewModel {Name = "Thomas Edward Lee"},
                    new ContactViewModel {Name = "Sergio Rios"},
                    new ContactViewModel {Name = "Yoselin Vega"},
                    new ContactViewModel {Name = "William Porras Quiros"},
                    new ContactViewModel {Name = "Alexander Remedios"},
                    new ContactViewModel {Name = "Alonso Blanco"},
                    new ContactViewModel {Name = "Bayron Chavarria"},
                    new ContactViewModel {Name = "Rosita"},
                    new ContactViewModel {Name = "Laura Rojas"},
                    new ContactViewModel {Name = "Walter"}
                }
            };
        }
    }
}