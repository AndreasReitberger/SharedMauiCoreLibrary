using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Contacts
{
    public partial class Contact : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Name))]
        public partial string FirstName { get; set; } = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Name))]
        public partial string LastName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial DateTimeOffset? TimeAdded { get; set; }

        [ObservableProperty]
        public partial Dictionary<string, string> PhoneNumbers { get; set; } = [];

        [ObservableProperty]
        public partial Dictionary<string, string> EmailAddresses { get; set; } = [];

        public string Name => string.IsNullOrEmpty(FirstName) ? $"{LastName}" : $"{LastName}, {FirstName}";
        #endregion

        #region Ctor
        public Contact() { }

        #endregion
    }
}
