namespace LuckyWinner.Pages
{
    using System;
    using Views;
    using System.Diagnostics;
	using Xamarin.Forms;

    public partial class MainPage
	{
		public MainPage ()
		{
		    try
		    {
                InitializeComponent();

				Detail = new NavigationPage(new ContentPage { Title = Title, Content = new GameView()});
            }
            catch (Exception ex)
		    {
                Debug.WriteLine(ex);
                throw;
            }
        }
	}
}