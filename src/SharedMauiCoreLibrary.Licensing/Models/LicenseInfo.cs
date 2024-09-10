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
        string domain = string.Empty;

        [ObservableProperty]
        string license = string.Empty;

        [ObservableProperty]
        string owner = string.Empty;

        [ObservableProperty]
        string productCode = string.Empty;

        [ObservableProperty]
        bool isValid = false;

        [ObservableProperty]
        bool isActive = false;

        [ObservableProperty]
        DateTimeOffset lastCheck;

        [ObservableProperty]
        ILicenseOptions? options;
        #endregion

        #region Ctor
        public LicenseInfo()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
