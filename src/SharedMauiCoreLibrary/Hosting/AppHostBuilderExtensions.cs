using AndreasReitberger.Shared.Core.Interfaces;
using AndreasReitberger.Shared.Core.Localization;
using AndreasReitberger.Shared.Core.NavigationManager;
using CommunityToolkit.Maui;
using System.Runtime.Versioning;

namespace AndreasReitberger.Shared.Core.Hosting
{
    [SupportedOSPlatform(SPlatforms.AndroidVersion)]
    [SupportedOSPlatform(SPlatforms.IOSVersion)]
    [SupportedOSPlatform(SPlatforms.MACCatalystVersion)]
    [SupportedOSPlatform(SPlatforms.WindowsVersion)]
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

        public static MauiAppBuilder ConfigureShellNavigator(this MauiAppBuilder builder, string rootPage, List<string>? entryPages = null, IDispatcher? dispatcher = null)
        {
            dispatcher ??= builder.Services.BuildServiceProvider().GetService<IDispatcher>();
            ShellNavigator navigator = dispatcher is not null ? new(rootPage, dispatcher) : new(rootPage);
            navigator.AvailableEntryPages = [.. entryPages ?? [ rootPage]];
            builder.Services.AddSingleton<IShellNavigator>(navigator);
            return builder;
        }
    }
}
