using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing.Models
{
    public partial class LicenseOptions : ObservableObject
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

