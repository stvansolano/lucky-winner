namespace Shared
{
    public class User
    {
		public User ()
		{
			Id = string.Empty;
			Name = string.Empty;
			Email = string.Empty;
			RegistrationKey = string.Empty;
		}

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RegistrationKey { get; set; }
    }
}