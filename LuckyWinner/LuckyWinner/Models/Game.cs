namespace Shared
{
    using System.Collections.Generic;

    public class Game
    {
		public Game ()
		{
			Id = string.Empty;
			Name = string.Empty;
			Owner = new User ();
			Participants = new List<string> ();
			History = new List<string> ();
		}

        public string Id { get; set; }
        public string Name { get; set; }
        public User Owner{ get; set; }
        public List<string> Participants { get; set; }
		public List<string> History { get; set; }
    }
}