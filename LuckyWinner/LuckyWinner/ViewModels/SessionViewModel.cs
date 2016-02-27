using System.Collections.ObjectModel;

namespace Shared.ViewModels
{
    // Session
    //      -> User {email, name, etc}
    //      -> OwnedGames []
    //          -> GameType
    //          -> Name
    //          -> History
    //          -> Participants
    //      -> Subscriptions
    //          -> Id

    public class SessionViewModel
    {
        public UserViewModel User { get; set; }
        public ObservableCollection<GameViewModel> Games { get; private set; }
        public ObservableCollection<SubscriptionViewModel> Subscriptions { get; private set; }

		public SessionViewModel(UserService service)
        {
            Service = service;
			Games = new ObservableCollection<GameViewModel> ();
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

    public class UserViewModel : ViewModelBase
    {
        public UserViewModel(User model)
        {
            Id = model.Id;
            Email = model.Email;
            Name = model.Name;

			Model = model;
        }

		public User Model { 
			get;
			private set;
		}

        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}