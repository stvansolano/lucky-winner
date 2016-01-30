using System;

using Xamarin.Forms;

namespace LuckyWinner
{
	public class MenuPage : ContentPage
	{
		public MenuPage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


