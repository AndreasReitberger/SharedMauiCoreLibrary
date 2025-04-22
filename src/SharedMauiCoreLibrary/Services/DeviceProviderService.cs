namespace AndreasReitberger.Shared.Core.Services
{
    // Somehow has trouble with .NET 6
#if ANDROID || IOS || MACCATALYST || WINDOWS
    public partial class DeviceProviderService
    {
        public static partial string? GetDeviceId();
    }
#endif
}
