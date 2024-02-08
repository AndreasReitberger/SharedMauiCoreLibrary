using AndreasReitberger.Shared.Core.Interfaces;
using AndreasReitberger.Shared.Core.Localization;
using CommunityToolkit.Maui;

namespace AndreasReitberger.Shared.Core.Hosting
{
    public static class AppHostBuilderExtensions
    {
        public static MauiAppBuilder ConfigureCoreLibrary(this MauiAppBuilder builder)
        {
            builder.UseMauiCommunityToolkit();
            return builder;
        }

        public static MauiAppBuilder ConfigureLocalizationManager(this MauiAppBuilder builder, List<LocalizationInfo> languages, string flagImageBaseDir = "")
        {
            LocalizationManager manager = new()
            {
                BaseFlagImageUri = flagImageBaseDir
            };
            manager.SetLanguages(languages);
            builder.Services.AddSingleton<ILocalizationManager>(manager);
            return builder;
        }

        public static MauiAppBuilder ConfigureLocalizationManager(this MauiAppBuilder builder, string flagImageBaseDir = "")
        {
            LocalizationManager manager = new()
            {
                BaseFlagImageUri = flagImageBaseDir
            };
            builder.Services.AddSingleton<ILocalizationManager>(manager);
            return builder;
        }

    }
}
