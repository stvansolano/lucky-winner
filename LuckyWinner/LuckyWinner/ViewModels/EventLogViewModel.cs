namespace Shared.ViewModels
{
    using System;

    public class EventLogViewModel : ViewModelBase
    {
        public EventLogViewModel(string message)
        {
            Description = message;
        }

        public DateTime Created { get; set; }
        public string Description { get; set; }
    }
}