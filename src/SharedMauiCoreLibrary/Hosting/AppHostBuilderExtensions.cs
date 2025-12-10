using AndreasReitberger.Shared.Core.Interfaces;
using AndreasReitberger.Shared.Core.Localization;
using AndreasReitberger.Shared.Core.Dispatch;
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
        /// <summary>
        /// Configures the core library features for a .NET MAUI application using the specified builder.
        /// Included features:
        /// - UseMauiCommunityToolkit()
        /// - ConfigureDispatching()
        /// </summary>
        /// <remarks>This method adds essential services and features from the core library, including the
        /// .NET MAUI Community Toolkit and dispatching support. Call this method during application startup to ensure
        /// required functionality is available throughout the app.</remarks>
        /// <param name="builder">The <see cref="MauiAppBuilder"/> instance to configure. Cannot be null.</param>
        /// <returns>The same <see cref="MauiAppBuilder"/> instance, enabling method chaining.</returns>
        public static MauiAppBuilder ConfigureCoreLibrary(this MauiAppBuilder builder)
        {
            builder
                .UseMauiCommunityToolkit()
                .ConfigureDispatching()
                ;
            return builder;
        }

        public static MauiAppBuilder RegisterDispatcher(this MauiAppBuilder builder)
        {
            if (Dispatcher.GetForCurrentThread() is IDispatcher dispatcher)
            {
                builder.Services.AddSingleton<IDispatcher>(dispatcher);
                builder.Services.AddSingleton<IDispatchManager>(new DispatchManager(dispatcher));
            }
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
