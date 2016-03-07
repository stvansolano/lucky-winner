namespace Shared
{
    using System.Collections.Generic;

    public class Game
    {
		public Game ()
		{
			Id = string.Empty;
			Name = string.Empty;
			Owner = string.Empty;
			Participants = new List<string> ();
			History = new List<string> ();
		}

        public string Id { get; set; }
        public string Name { get; set; }
        public string Owner{ get; set; }
        public List<string> Participants { get; set; }
		public List<string> History { get; set; }
    }
}