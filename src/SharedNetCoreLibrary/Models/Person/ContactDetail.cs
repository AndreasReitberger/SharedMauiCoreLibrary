using AndreasReitberger.Shared.Core.Enums;
using AndreasReitberger.Shared.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Person
{
    public partial class ContactDetail : ObservableObject, IContactDetail
    {
        #region Properties

        [ObservableProperty]
        public partial ContactType Type { get; set; }

        [ObservableProperty]
        public partial string Value { get; set; } = string.Empty;
        #endregion
    }
}
