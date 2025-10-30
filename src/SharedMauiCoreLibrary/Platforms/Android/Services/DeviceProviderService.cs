using System.Runtime.Versioning;
using static Android.Provider.Settings;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class DeviceProviderService
    {
        [SupportedOSPlatform(SPlatforms.AndroidVersion)]
        public static partial string? GetDeviceId()
            => Secure.GetString(Android.App.Application.Context?.ContentResolver, Secure.AndroidId);
    }
}
