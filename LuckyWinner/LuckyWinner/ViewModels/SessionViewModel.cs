namespace Shared.ViewModels
{
	using System.Collections.ObjectModel;

    public class SessionViewModel
    {
        public UserViewModel User { get; set; }
        public ObservableCollection<SubscriptionViewModel> Subscriptions { get; private set; }

		public SessionViewModel(UserService service)
        {
            Service = service;
			Subscriptions = new ObservableCollection<SubscriptionViewModel> ();

			User = new UserViewModel (new User());
		}

        public UserService Service { get; set; }
    }

    public class SubscriptionViewModel : ViewModelBase
    {
        public string Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}