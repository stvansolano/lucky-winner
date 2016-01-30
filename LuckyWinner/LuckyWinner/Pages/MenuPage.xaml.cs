namespace LuckyWinner.Pages
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Xamarin.Forms;
	using System.Linq;
	using Views;

	public partial class MenuPage
	{
		public MenuPage ()
		{
			InitializeComponent ();

			var options = new ObservableCollection<View>();
			options.Add(new GameView());

			MenuOptions = options;

			CurrentView = MenuOptions.First();
		}

		public IEnumerable<View> MenuOptions { get; set; }

		public View CurrentView { get; set; }
	}
}