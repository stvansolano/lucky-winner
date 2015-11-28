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

            Players = new ObservableCollection<PlayerViewModel>();

            FillPlayers(new string [] {
                "Carlos Argüello",
                "Marco Antonio Gómez",
                "Daniel Lacayo",
                "Carlos Cervantes Gutierrez",
                "Geraldin Torres",
                "Xiannie Hylton",
                "May Valverde",
                "Richard Fernández",
                "Herber Madrigal",
                "Oscar Arroyo Fuentes",
                "Aaron Cedeño Martí­nez",
                "Andrés Carvajal",

                "Andrey Alfaro",
                "JC",
                "Alejandra Herrera P",
                "Cesar Guillen Oreamuno",
                "Jairo Esquivel",
                "Stuart Sanchez",
                "Kenneth P. Barquero",
                "Kimberly",
                "Esteban (estsantre96@hotmail.com)",

                "Deibyd",
                "Esteban Hernandez",
                "Javier Nuñez",
                "Jose Alberto Gonzalez Sanchez",
                "Keneth Murillo",
                "Pablo Duran",
                "Pablo Salas",
                "Roberto Chacón",
                "Steven",
                "Thomas Edward Lee",
                "Sergio Rios",
                "Yoselin Vega",
                "William Porras Quiros",
                "Alexander Remedios",
                "Alonso Blanco",
                "Bayron Chavarria",
                "Rosita",
                "Laura Rojas",
                "Walter",
            });
        }

        private void FillPlayers(string[] items)
        {
            Players.Clear();

            foreach (var player in items)
            {
                Players.Add(new PlayerViewModel {PlayerName = player});
            }
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