using UIKit;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class DeviceProviderService
    {
        public partial string? GetDeviceId()
            => UIDevice.CurrentDevice?.IdentifierForVendor?.ToString();
    }
}
