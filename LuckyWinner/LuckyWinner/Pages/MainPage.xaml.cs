namespace LuckyWinner.Pages
{
    using System;
    using Views;
    using System.Diagnostics;

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
                Debug.WriteLine(ex);
                throw;
            }
        }
	}
}