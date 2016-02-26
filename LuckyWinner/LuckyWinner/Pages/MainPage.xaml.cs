namespace LuckyWinner.Pages
{
    using System;
    using Views;
    using System.Diagnostics;
	using Xamarin.Forms;
    using Shared;
    using Shared.ViewModels;

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
                InitializeComponent();

                Network = new NetworkService();

		        UserAuth = new UserService(Network);
                Session = new SessionViewModel(UserAuth);

                this.RunSafe(() => CheckinUser());

				MainGame = new GameView();

				var viewModel = new GameViewModel(GameService, new Game());

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
            catch (Exception ex)
		    {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private async void CheckinUser()
        {
            object registrationKey;
            if (KeyValueStore.TryGetValue("registrationKey", out registrationKey) == false)
            {
                registrationKey = string.Empty;
            }

            var user = await UserAuth.Checkin(registrationKey.ToString());

			KeyValueStore.Set("registrationKey", user.RegistrationKey);

            Session.User = new UserViewModel(user);
        }
	}
}