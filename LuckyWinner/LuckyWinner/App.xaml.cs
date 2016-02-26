using System.Collections.Generic;

[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
namespace LuckyWinner
{
    using Pages;

    public partial class App
	{
		public App ()
		{
			InitializeComponent ();

			MainPage = new MainPage(new AppKeyValueStore(Properties));
		}

        public AppKeyValueStore KeyValueStore { get; set; }

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

    public class AppKeyValueStore
    {
        private readonly IDictionary<string, object> _properties;

        public AppKeyValueStore(IDictionary<string, object> properties)
        {
            _properties = properties;
        }

		public bool TryGetValue (string key, out object value)
		{
			return _properties.TryGetValue (key, out value);
		}

		public void Set (string key, object value)
		{
			if (_properties.ContainsKey(key)) {
				_properties [key] = value;
				return;
			}
			_properties.Add (key, value);
		}
    }
}