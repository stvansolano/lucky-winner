namespace Shared
{
    using System.Collections.Generic;

    public class Game
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public User Owner{ get; set; }
        public List<User> Participants { get; set; }
    }
}