using Windows.Security.Cryptography;
using Windows.System.Profile;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class DeviceProviderService
    {
        public partial string? GetDeviceId()
        {
            SystemIdentificationInfo? id = SystemIdentification.GetSystemIdForPublisher();
            return CryptographicBuffer.EncodeToHexString(id?.Id);
        }
    }
}
