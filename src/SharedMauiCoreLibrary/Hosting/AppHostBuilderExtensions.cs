using AndreasReitberger.Shared.Core.Dispatch;
using AndreasReitberger.Shared.Core.Events;
using AndreasReitberger.Shared.Core.Interfaces;
using AndreasReitberger.Shared.Core.Localization;
using AndreasReitberger.Shared.Core.NavigationManager;
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics;
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
                .RegisterDispatcher()
                ;
            return builder;
        }

        public static MauiAppBuilder RegisterDispatcher(this MauiAppBuilder builder, IDispatcher? dispatcher = null)
        {
            dispatcher ??= builder.Services.BuildServiceProvider().GetService<IDispatcher>();
            if (dispatcher is not null)
            {
                DispatchManager manager = new(dispatcher);
#if DEBUG
                manager.Error += (a, b) =>
                {
                    if (b is DispatchErrorEventArgs args)
                        Debug.WriteLine($"{nameof(DispatchManager)}: Dispatch error occured: \n{args}");
                };
#endif
                builder.Services.TryAddSingleton<IDispatchManager>(manager);
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
            builder.Services.TryAddSingleton<ILocalizationManager>(manager);
            return builder;
        }

        public static MauiAppBuilder ConfigureLocalizationManager(this MauiAppBuilder builder, string flagImageBaseDir = "")
        {
            LocalizationManager manager = new()
            {
                BaseFlagImageUri = flagImageBaseDir
            };
            builder.Services.TryAddSingleton<ILocalizationManager>(manager);
            return builder;
        }

        public static MauiAppBuilder ConfigureShellNavigator(this MauiAppBuilder builder, string rootPage, List<string>? entryPages = null, IDispatcher? dispatcher = null)
        {
            dispatcher ??= builder.Services.BuildServiceProvider().GetService<IDispatcher>();
            ShellNavigator navigator = dispatcher is not null ? new(rootPage, dispatcher) : new(rootPage);
            navigator.AvailableEntryPages = [.. entryPages ?? [ rootPage]];
            builder.Services.TryAddSingleton<IShellNavigator>(navigator);
            return builder;
        }
    }
}
