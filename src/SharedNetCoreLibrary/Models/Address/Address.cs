using CommunityToolkit.Mvvm.ComponentModel;
using AndreasReitberger.Shared.Core.Interfaces;

namespace AndreasReitberger.Shared.Core.Models.Adress
{
    public partial class Address : ObservableObject, IAddress
    {
        #region Properties
        [ObservableProperty]
        public partial string Street { get; set; }  

        [ObservableProperty]
        public partial string HouseNumber { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string ZipCode { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string City { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string State { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Country { get; set; } = string.Empty;
        #endregion
    }
}
