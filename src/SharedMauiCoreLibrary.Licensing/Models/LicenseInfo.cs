using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing
{
    public partial class LicenseInfo : ObservableObject, ILicenseInfo
    {

        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Domain { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string License { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Owner { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string ProductCode { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool IsValid { get; set; } = false;

        [ObservableProperty]
        public partial bool IsActive { get; set; } = false;

        [ObservableProperty]
        public partial DateTimeOffset LastCheck { get; set; }

        [ObservableProperty]
        public partial ILicenseOptions? Options { get; set; }
        #endregion

        #region Ctor
        public LicenseInfo()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
