using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing
{
    public partial class LicenseInfo : ObservableObject, ILicenseInfo
    {

        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string domain;

        [ObservableProperty]
        string license;

        [ObservableProperty]
        string owner;

        [ObservableProperty]
        string productCode;

        [ObservableProperty]
        bool isValid;

        [ObservableProperty]
        bool isActive;

        [ObservableProperty]
        DateTimeOffset lastCheck;

        [ObservableProperty]
        ILicenseOptions options;
        #endregion

        #region Ctor
        public LicenseInfo()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
