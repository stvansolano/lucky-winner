namespace Shared.ViewModels
{
	using System.Collections.ObjectModel;

    public class SessionViewModel : ViewModelBase
    {
        public UserViewModel User { get; set; }
        public ObservableCollection<SubscriptionViewModel> Subscriptions { get; private set; }
		protected GameService GameService { get; set; }
		public UserService UserAuth { get; protected set; }

		protected SessionViewModel(NetworkService network)
        {
			Subscriptions = new ObservableCollection<SubscriptionViewModel> ();

			User = new UserViewModel (new User());
			UserAuth = new UserService(network);
			GameService = new GameService(network);
		}
    }

    public class SubscriptionViewModel : ViewModelBase
    {
        public string Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}