using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing.Models
{
    public partial class LicenseOptions : ObservableObject, ILicenseOptions
    {
        #region Properties
        [ObservableProperty]
        string productName = string.Empty;

        [ObservableProperty]
        string productIdentifier = string.Empty;

        [ObservableProperty]
        string licenseCheckPattern = string.Empty;

        [ObservableProperty]
        bool verifyLicenseFormat;
        #endregion

        #region Ctor
        public LicenseOptions()
        {
        }
        #endregion
    }
}

