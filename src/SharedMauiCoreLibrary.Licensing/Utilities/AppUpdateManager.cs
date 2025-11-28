using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using AndreasReitberger.Shared.Core.Update;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing.Utilities
{
    public abstract partial class AppUpdateManager : UpdateManager, IAppUpdateManager
    {
        #region Properties

        [ObservableProperty]
        public partial IDispatcher? Dispatcher { get; set; }

        [ObservableProperty]
        public partial ILicenseManager? LicenseManager { get; set; }

        [ObservableProperty]
        public partial string ProductCode { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Domain { get; set; } = string.Empty;

        #endregion

        #region Ctor
        public AppUpdateManager(ILicenseManager manager) : base()
        {
            Dispatcher ??= Application.Current?.Dispatcher;
            LicenseManager = manager;
        }
        public AppUpdateManager(IDispatcher dispatcher, ILicenseManager manager) : base()
        {
            Dispatcher = dispatcher;
            LicenseManager = manager;
        }
        #endregion

        #region Methods
        public async Task<bool> CheckForUpdateAsync()
        {
            if (LicenseManager is null)
            {
                OnClientIncompatibleWithNewVersion();
                return false;
            }
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                IApplicationVersionResult? res = await LicenseManager?.GetLatestApplicationVersionAsync(domain: Domain, productCode: ProductCode, target: Enums.LicenseServerTarget.WooCommerce, null, null);
            }
            return true;
        }
        #endregion
    }
}
