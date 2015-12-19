namespace LuckyWinner.Views
{
    using System;
    using Shared.ViewModels;
    using Xamarin.Forms;
    using System.Linq;
    using Shared;
    using System.Diagnostics;

    public partial class RaffleView
    {
        public RaffleView()
		{
            try
            {
                ViewModel = new RaffleViewModel(new NetworkService());
                ViewModel.PlayCommand = new Command(() => Play());

                InitializeComponent();

                NewPlayerEntry.Completed += (sender, args) =>
                {
                    ViewModel.Players.Add(GetNewPlayer(NewPlayerEntry.Text));
                    NewPlayerEntry.Text = string.Empty;

					NewPlayerEntry.Focus();
                };

                foreach (var item in ViewModel.Players)
                {
                    SetCommands(item);
                }

                FillPicker();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
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

				PlayersSelector.ScrollTo(selectedPlayer, ScrollToPosition.Center, true);
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

        private RaffleViewModel _viewModel;
        public RaffleViewModel ViewModel
	    {
	        get { return _viewModel; }
	        set { _viewModel = value; }
	    }

        public override string ToString()
        {
            return ViewModel.Title;
        }

        public void FillPicker()
        {
            var viewModel = ViewModel;

            foreach (var item in ViewModel.GroupNames)
            {
                GroupPicker.Items.Add(item);
            }

            GroupPicker.SelectedIndexChanged += (sender, args) =>
            {
                ViewModel.SelectedGroup = viewModel.Groups.ElementAtOrDefault(GroupPicker.SelectedIndex);
            };
        }
    }
}