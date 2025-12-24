namespace AndreasReitberger.Shared.Core.Utilities
{
    public partial class ImageHelper
    {
        #region Methods

        public static byte[] ToArray(Microsoft.Maui.Graphics.IImage image)
        {
            using MemoryStream memory = new();
            image.Save(memory);
            return memory.ToArray();
        }

        public static byte[] ToArray(Image image)
        {
            // Based on https://github.com/CommunityToolkit/Maui/blob/main/src/CommunityToolkit.Maui/Converters/ByteArrayToImageSourceConverter.shared.cs
            if (image?.Source is not StreamImageSource streamImageSource)
                return [];

            using MemoryStream memory = new();
            Stream? streamFromImageSource = streamImageSource.Stream(CancellationToken.None).GetAwaiter().GetResult();
            if (streamFromImageSource is null)
                return [];
            streamFromImageSource.CopyTo(memory);
            return memory.ToArray();
        }
        #endregion
    }
}
