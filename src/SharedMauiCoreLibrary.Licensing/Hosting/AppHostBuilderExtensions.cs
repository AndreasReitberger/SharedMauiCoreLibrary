using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using AndreasReitberger.Shared.Core.Licensing.Utilities;

namespace AndreasReitberger.Shared.Core.Licensing.Hosting
{
    public static class AppHostBuilderExtensions
    {
        public static MauiAppBuilder ConfigureLicensing(this MauiAppBuilder builder, Uri licenseServer, string accessToken)
        {
            LicenseManager manager = new LicenseManager.LicenseManagerConnectionBuilder()
                .WithLicenseServer(licenseServer)
                .WithAccessToken(accessToken)
                .Build();
            builder.Services.AddSingleton<ILicenseManager>(manager);
            return builder;
        }
        public static MauiAppBuilder ConfigureAppUpdater(this MauiAppBuilder builder, ILicenseManager licenseManager, IDispatcher? dispatcher = null)
        {
            dispatcher ??= builder.Services.BuildServiceProvider().GetService<IDispatcher>();
            AppUpdateManager manager = dispatcher is null ? new(licenseManager) : new(dispatcher, licenseManager);
            builder.Services.AddSingleton<IAppUpdateManager>(manager);
            return builder;
        }
        public static MauiAppBuilder ConfigureAppUpdater(this MauiAppBuilder builder, IDispatcher? dispatcher = null)
        {
            ILicenseManager? licenseManager = builder.Services.BuildServiceProvider().GetService<ILicenseManager>();
            dispatcher ??= builder.Services.BuildServiceProvider().GetService<IDispatcher>();
            if (licenseManager is not null && dispatcher is not null)
            {
                builder.ConfigureAppUpdater(licenseManager, dispatcher);
            }
            return builder;
        }
    }
}
