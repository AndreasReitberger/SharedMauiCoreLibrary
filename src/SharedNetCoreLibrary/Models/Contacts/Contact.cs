using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Contacts
{
    public partial class Contact : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Name))]
        string firstName = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Name))]
        string lastName = string.Empty;

        [ObservableProperty]
        DateTimeOffset? timeAdded;

        [ObservableProperty]
        Dictionary<string, string> phoneNumbers = [];

        [ObservableProperty]
        Dictionary<string, string> emailAddresses = [];

        public string Name => string.IsNullOrEmpty(FirstName) ? $"{LastName}" : $"{LastName}, {FirstName}";
        #endregion

        #region Ctor
        public Contact() { }

        #endregion
    }
}
