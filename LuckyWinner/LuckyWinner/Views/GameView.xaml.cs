namespace LuckyWinner.Views
{
    using System;
    using Shared.ViewModels;
    using Xamarin.Forms;
    using System.Linq;
    using System.Diagnostics;

    public partial class GameView
    {
        public GameView()
		{
            try
            {
                InitializeComponent();

                NewPlayerEntry.Completed += (sender, args) =>
                {
                    ViewModel.AddPlayer(NewPlayerEntry.Text);
                    NewPlayerEntry.Text = string.Empty;

					NewPlayerEntry.Focus();
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private GameViewModel _viewModel;
        public GameViewModel ViewModel
	    {
	        get { return _viewModel; }
	        set {
				_viewModel = value;
				OnPropertyChanged ("ViewModel");
			}
	    }

        public override string ToString()
        {
            return ViewModel.Title;
        }

        public void Reveal(PlayerViewModel winner)
        {
            PlayersSelector.ScrollTo(winner, ScrollToPosition.Center, true);
        }
    }
}