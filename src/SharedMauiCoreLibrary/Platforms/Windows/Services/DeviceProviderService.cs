using System.Runtime.Versioning;
using Windows.Security.Cryptography;
using Windows.System.Profile;

namespace AndreasReitberger.Shared.Core.Services
{
    [SupportedOSPlatform(SPlatforms.WindowsVersion)]
    public partial class DeviceProviderService
    {
        [SupportedOSPlatform(SPlatforms.WindowsVersion)]
        public static partial string? GetDeviceId()
        {
            SystemIdentificationInfo? id = SystemIdentification.GetSystemIdForPublisher();
            return CryptographicBuffer.EncodeToHexString(id?.Id);
        }
    }
}
