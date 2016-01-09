namespace Shared.ViewModels
{
    using System.Collections.Generic;

    public class GroupViewModel : ViewModelBase
    {
        public string GroupName { get; set; }

        public IEnumerable<ContactViewModel> Contacts { get; set; }
    }

    public class ContactViewModel : ViewModelBase
    {
        public string Name { get; set; }
    }
}