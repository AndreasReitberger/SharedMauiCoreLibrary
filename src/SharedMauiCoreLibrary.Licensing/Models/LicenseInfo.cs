using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing.Models {
    public partial class LicenseInfo : ObservableObject, ILicenseInfo {

        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string license;

        [ObservableProperty]
        string owner;

        [ObservableProperty]
        string application;

        [ObservableProperty]
        bool isValid;

        [ObservableProperty]
        bool isActive;

        [ObservableProperty]
        DateTimeOffset lastCheck;
        #endregion
    }
}
