namespace AndreasReitberger.Shared.Core.Services
{
    // Somehow has trouble with .NET 6
#if ANDROID || IOS || MACCATALYST || WINDOWS
    public partial class ImageResizerService
    {
        public partial byte[] ResizeImage(byte[] imageData, float width, float height = -1);
    }
#endif
}
