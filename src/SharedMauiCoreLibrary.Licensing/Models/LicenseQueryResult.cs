using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing
{
    public partial class LicenseQueryResult : ObservableObject, ILicenseQueryResult
    {
        #region Properties

        [ObservableProperty]
        bool success = false;

        [ObservableProperty]
        bool valid = false;

        [ObservableProperty]
        DateTimeOffset timeStamp;

        [ObservableProperty]
        string message = string.Empty;

        #endregion
    }
}
