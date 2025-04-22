using UIKit;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class DeviceProviderService
    {
        public static partial string? GetDeviceId()
            => UIDevice.CurrentDevice?.IdentifierForVendor?.ToString();
    }
}
