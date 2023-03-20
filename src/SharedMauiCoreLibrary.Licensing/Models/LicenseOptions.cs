using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing.Models
{
    public partial class LicenseOptions : ObservableObject, ILicenseOptions
    {
        #region Properties
        [ObservableProperty]
        string productName;

        [ObservableProperty]
        string productIdentifier;

        [ObservableProperty]
        string licenseCheckPattern;

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

