namespace LuckyWinner.Pages
{
    using System;
    using Views;
    using System.Diagnostics;
	using Xamarin.Forms;
    using Shared;
    using Shared.ViewModels;
	using System.Threading.Tasks;

    public partial class MainPage
	{
        protected GameService GameService { get; set; }
        protected GameView MainGame { get; set; }
        protected UserService UserAuth { get; set; }
        protected NetworkService Network { get; set; }
        protected SessionViewModel Session { get; set; }
        protected AppKeyValueStore KeyValueStore { get; set; }

        public MainPage (AppKeyValueStore keyValueStore)
        {
            KeyValueStore = keyValueStore;
            
		    try
		    {
				Network = new NetworkService();
				UserAuth = new UserService(Network);
				Session = new SessionViewModel(UserAuth);
				GameService = new GameService(Network);

                InitializeComponent();

				InitializeUserInterface();
				InitializeUserSettings();
            }
            catch (Exception ex)
		    {
                Debug.WriteLine(ex);
                throw;
            }
        }

		private void InitializeUserInterface ()
		{
			MainGame = new GameView();

			var viewModel = new GameViewModel(GameService);

			viewModel.PlayCommand = new Command(() =>
				{
					viewModel.Play();

					var winner = viewModel.Winner;
					if (winner != null)
					{
						MainGame.Reveal(winner);
					}
				});

			MainGame.ViewModel = viewModel;
			Detail = new NavigationPage(new ContentPage { Title = MainGame.ViewModel.Title, Content = MainGame});
		}

		private const string REGISTRATION_KEY = "RegistrationKey";

		private async void InitializeUserSettings()
        {
            object registrationKey;
			if (KeyValueStore.TryGetValue(REGISTRATION_KEY, out registrationKey) == false)
            {
                registrationKey = string.Empty;
            }

            var user = await UserAuth.Checkin(registrationKey.ToString());

			KeyValueStore.Set(REGISTRATION_KEY, user.RegistrationKey);

            Session.User = new UserViewModel(user);
			Session.Games.Add (MainGame.ViewModel);

			MainGame.ViewModel.Model.Owner = Session.User.Model;
			await GameService.SaveAsync (MainGame.ViewModel.Model);
        }
	}
}