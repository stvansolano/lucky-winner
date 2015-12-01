namespace LuckyWinner.Views
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;
    using System.Linq;

    public partial class SideMenuView
	{
		public SideMenuView ()
		{
			InitializeComponent ();

            var options = new ObservableCollection<View>();
            options.Add(new RaffleView());

		    MenuOptions = options;

		    CurrentView = MenuOptions.First();
		}

	    public IEnumerable<View> MenuOptions { get; set; }

	    public View CurrentView { get; set; }
	}
}