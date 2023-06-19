using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using Microsoft.Maui.Controls.Compatibility.Hosting;

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
            //builder.UseMauiCompatibility();
            return builder;
        }
    }
}
