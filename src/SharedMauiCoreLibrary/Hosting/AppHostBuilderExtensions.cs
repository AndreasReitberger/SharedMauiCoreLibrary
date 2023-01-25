using Microsoft.Maui.Controls.Compatibility.Hosting;

namespace AndreasReitberger.Shared.Core.Hosting
{
    public static class AppHostBuilderExtensions
    {
        public static MauiAppBuilder ConfigureCoreLibrary(this MauiAppBuilder builder)
        {
            builder.UseMauiCompatibility();
            return builder;
        }
    }
}
