namespace Shared
{
	using System.Collections.Generic;
	using System.Linq;

    public class User
    {
		public User ()
		{
			Id = string.Empty;
			Name = string.Empty;
			Email = string.Empty;
			RegistrationKey = string.Empty;

			OwnedGames = new List<string> ();
		}

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RegistrationKey { get; set; }


		public bool HasGames {
			get { return OwnedGames.Any(); }
		}

		public List<string> OwnedGames{ get; private set; }
    }
}