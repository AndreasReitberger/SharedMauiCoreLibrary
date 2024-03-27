using AndreasReitberger.Shared.Core.Interfaces;
using AndreasReitberger.Shared.Core.Localization;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;

namespace AndreasReitberger.Shared.Core.Hosting
{
    public static class AppHostBuilderExtensions
    {
        public static MauiAppBuilder ConfigureCoreLibrary(this MauiAppBuilder builder)
        {
            builder.UseMauiCommunityToolkit();
            builder.ConfigureFileServices();
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

        public static MauiAppBuilder ConfigureFileServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);
            builder.Services.AddSingleton<ILauncher>(Launcher.Default);
            return builder;
        }

    }
}
