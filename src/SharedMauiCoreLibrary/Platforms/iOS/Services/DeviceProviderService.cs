using UIKit;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class DeviceProviderService
    {
        public partial string GetDeviceId()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor?.ToString();
        }
    }
}
