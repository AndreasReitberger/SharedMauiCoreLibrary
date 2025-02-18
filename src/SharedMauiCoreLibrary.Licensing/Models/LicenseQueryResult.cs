using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing
{
    public partial class LicenseQueryResult : ObservableObject, ILicenseQueryResult
    {
        #region Properties

        [ObservableProperty]
        public partial bool Success { get; set; } = false;

        [ObservableProperty]
        public partial bool Valid { get; set; } = false;

        [ObservableProperty]
        public partial DateTimeOffset TimeStamp { get; set; }

        [ObservableProperty]
        public partial string Message { get; set; } = string.Empty;

        #endregion
    }
}
