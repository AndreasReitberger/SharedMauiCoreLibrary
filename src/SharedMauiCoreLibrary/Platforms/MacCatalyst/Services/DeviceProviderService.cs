using System.Runtime.Versioning;
using UIKit;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class DeviceProviderService
    {
        [SupportedOSPlatform(SPlatforms.IOSVersion)]
        [SupportedOSPlatform(SPlatforms.MACCatalystVersion)]
        public static partial string? GetDeviceId()
            => UIDevice.CurrentDevice?.IdentifierForVendor?.ToString();

    }
}
