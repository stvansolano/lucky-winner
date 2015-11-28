namespace LuckyWinner.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Xamarin.Forms.Platform.Android;

    [Activity (Label = "LuckyWinner", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@style/MyTheme")]
	public class MainActivity : /*FormsApplicationActivity*/FormsAppCompatActivity
    {
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

            ToolbarResource = Resource.Layout.toolbar;
            TabLayoutResource = Resource.Layout.tabs;

            LoadApplication (new App ());
		}
	}
}