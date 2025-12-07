using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using AndreasReitberger.Shared.Core.Update;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing.Utilities
{
    public partial class AppUpdateManager : UpdateManager, IAppUpdateManager
    {
        #region Properties

        [ObservableProperty]
        public partial IDispatcher? Dispatcher { get; set; }

        [ObservableProperty]
        public partial ILicenseManager? LicenseManager { get; set; }

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
        public override async Task<bool> CheckForUpdateAsync(string productCode, string? domain = null)
        {
            domain ??= string.Empty;
            bool updateAvailable = false;
            if (LicenseManager is null)
            {
                OnClientIncompatibleWithNewVersion();
                return updateAvailable;
            }
            else if (IsCheckingForUpdates)
            {
                return updateAvailable;
            }
            IsCheckingForUpdates = true;
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                IApplicationVersionResult? res = await LicenseManager
                    .GetLatestApplicationVersionAsync(productCode: productCode, target: Enums.LicenseServerTarget.WooCommerce, null, null)
                    .ConfigureAwait(false);
                OnUpdateAvailable(new()
                {
                    LatestVersion = new(res?.Version ?? "0.0.0"),
                });
            }
            IsCheckingForUpdates = false;
            return updateAvailable;
        }
        #endregion
    }
}
