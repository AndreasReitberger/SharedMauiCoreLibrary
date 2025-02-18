using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing.Models
{
    public partial class LicenseOptions : ObservableObject, ILicenseOptions
    {
        #region Properties
        [ObservableProperty]
        public partial string ProductName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string ProductIdentifier { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string LicenseCheckPattern { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool VerifyLicenseFormat { get; set; }
        #endregion

        #region Ctor
        public LicenseOptions()
        {
        }
        #endregion
    }
}

