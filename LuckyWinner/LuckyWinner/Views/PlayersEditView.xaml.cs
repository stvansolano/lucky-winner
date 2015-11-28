using System.Linq;

namespace LuckyWinner.Views
{
    using System;
    using Shared.ViewModels;
    using Xamarin.Forms;

    public partial class PlayersEditView
	{
        public PlayersEditView ()
		{
            try
            {
                ViewModel = new PlayerEditViewModel();
                ViewModel.PlayCommand = new Command(() => Play());

                InitializeComponent();

                NewPlayerEntry.Completed += (sender, args) =>
                {
                    ViewModel.Players.Add(GetNewPlayer(NewPlayerEntry.Text));
                    NewPlayerEntry.Text = string.Empty;
                };

                foreach (var item in ViewModel.Players)
                {
                    SetCommands(item);
                }
            }
            catch (Exception ex)
            {
            }
		}

        private void Play()
        {
            if (ViewModel.Players.Count <= 1)
            {
                return;
            }
            foreach (var item in ViewModel.Players)
            {
                item.IsWinner = false;
            }

            var random = new Random(DateTime.Now.Millisecond);

            var lucky = random.Next(0, ViewModel.Players.Count);
            var selectedPlayer = ViewModel.Players.ElementAtOrDefault(lucky);

            if (selectedPlayer != null)
            {
                selectedPlayer.IsWinner = true;
                ViewModel.Winner = selectedPlayer;

                PlayersSelector.SelectedItem = selectedPlayer;
                PlayersSelector.ScrollTo(selectedPlayer, ScrollToPosition.MakeVisible, true);
            }
        }

        private PlayerViewModel GetNewPlayer(string text)
        {
            var result = new PlayerViewModel {PlayerName = text };

            SetCommands(result);

            return result;
        }

        private void SetCommands(PlayerViewModel item)
        {
            item.DeleteCommand = new Command(() => ViewModel.Players.Remove(item));
        }

        private PlayerEditViewModel _viewModel;
        public PlayerEditViewModel ViewModel
	    {
	        get { return _viewModel; }
	        set { _viewModel = value; }
	    }

        public override string ToString()
        {
            return ViewModel.Title;
        }
	}
}