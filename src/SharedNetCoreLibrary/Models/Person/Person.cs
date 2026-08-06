using AndreasReitberger.Shared.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Person
{
    public partial class Person : ObservableObject, IPerson
    {
        #region Properties
        [ObservableProperty]
        public partial string Salutation { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string FirstName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string LastName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial IAddress? Address { get; set; }

        [ObservableProperty]
        public partial ICollection<IContactDetail> ContactDetails { get; set; } = [];
        #endregion
    }
}
