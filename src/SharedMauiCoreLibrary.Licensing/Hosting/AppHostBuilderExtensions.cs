using Microsoft.Maui.Controls.Compatibility.Hosting;

namespace AndreasReitberger.Shared.Core.Licensing.Hosting
{
    public static class AppHostBuilderExtensions
    {
        public static MauiAppBuilder ConfigureLicensing(this MauiAppBuilder builder)
        {
            builder.UseMauiCompatibility();
            return builder;
        }
    }
}
