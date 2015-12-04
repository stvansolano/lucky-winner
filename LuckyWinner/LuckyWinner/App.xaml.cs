namespace LuckyWinner
{
	using Pages;
	using Xamarin.Forms;

	public partial class App
	{
		public App ()
		{
			InitializeComponent ();

			var style = Resources["NavigationStyle"] as Style;
			MainPage = new NavigationPage(new MainPage ()) { Style = style };
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}