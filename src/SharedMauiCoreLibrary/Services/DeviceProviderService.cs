namespace AndreasReitberger.Shared.Core.Services
{
        // Somehow has trouble with .NET 6
#if ANDROID || IOS || MACCATALYST || WINDOWS
    public partial class DeviceProviderService
    {
        public partial string GetDeviceId();
    }
#endif
}
