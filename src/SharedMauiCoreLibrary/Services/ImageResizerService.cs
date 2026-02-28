#if ANDROID || IOS || MACCATALYST || WINDOWS
using System.Runtime.Versioning;
#endif

namespace AndreasReitberger.Shared.Core.Services
{
    // Somehow has trouble with .NET 6
#if ANDROID || IOS || MACCATALYST || WINDOWS
    [SupportedOSPlatform(SPlatforms.AndroidVersion)]
    [SupportedOSPlatform(SPlatforms.IOSVersion)]
    [SupportedOSPlatform(SPlatforms.WindowsVersion)]
    [SupportedOSPlatform(SPlatforms.MACCatalystVersion)]
    public partial class ImageResizerService
    {
        public partial byte[] ResizeImage(byte[] imageData, float width, float height = -1);
    }
#endif
}
