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
        public UserViewModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Name = user.Name;
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
