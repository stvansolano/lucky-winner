namespace Shared.ViewModels
{
	public class UserViewModel : ViewModelBase
	{
		public UserViewModel(User model)
		{
			Id = model.Id;
			Email = model.Email;
			Name = model.Name;

			Model = model;
		}

		public User Model {  get; private set; }
		public string Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
	}
}