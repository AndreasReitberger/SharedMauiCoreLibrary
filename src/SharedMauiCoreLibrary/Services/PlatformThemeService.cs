namespace AndreasReitberger.Shared.Core.Services
{
    // Somehow has trouble with .NET 6
#if ANDROID || IOS || MACCATALYST || WINDOWS
    public partial class PlatformThemeService
    {
        // Based on: https://stackoverflow.com/a/39164921/10083577
        public static partial void SetStatusBarColor(Color color);
    }
#endif
}
