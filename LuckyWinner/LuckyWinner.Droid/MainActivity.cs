using System;
using Xamarin.Forms;

namespace LuckyWinner.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Xamarin.Forms.Platform.Android;

    [Activity(Label = "Lucky Winner", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@style/MyTheme")]
    public class MainActivity : /*FormsApplicationActivity*/FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                try
                {
                    var ex = ((Exception)e.ExceptionObject).GetBaseException();
                    //InsightsManager.Report(ex, Xamarin.Insights.Severity.Critical);

                    System.Diagnostics.Debug.WriteLine(ex);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            };

            try
            {
                base.OnCreate(bundle);

                Forms.Init(this, bundle);

                ToolbarResource = Resource.Layout.toolbar;
                TabLayoutResource = Resource.Layout.tabs;

                LoadApplication(new App());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}