using LuckyWinner.Views;

namespace LuckyWinner.Pages
{
    using System;

    public partial class MainPage
	{
		public MainPage ()
		{
		    try
		    {
                InitializeComponent();

		        var sideMenu = new SideMenuView();
                Menu.Content = sideMenu;

                CurrentPage.Content = sideMenu.CurrentView;
		    }
            catch (Exception ex)
		    {
		        throw;
		    }
		}
	}
}