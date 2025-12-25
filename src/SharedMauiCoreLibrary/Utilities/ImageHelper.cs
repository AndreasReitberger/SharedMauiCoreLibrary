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
        public static byte[] ToArray(FileStream imageStream)
        {
            if (imageStream is null)
                return [];
            using MemoryStream memory = new();
            imageStream.CopyTo(memory);
            return memory.ToArray();
        }
        public static async Task<byte[]> ToArrayAsync(FileStream imageStream)
        {
            if (imageStream is null)
                return [];
            using MemoryStream memory = new();
            await imageStream.CopyToAsync(memory);
            return memory.ToArray();
        }
        public static Task<byte[]> ToArrayAsync(string imagePath) => ToArrayAsync(GetImageStream(imagePath));
        public static FileStream GetImageStream(string filePath) => File.OpenRead(filePath);
        public static Task<FileStream> GetImageStreamAsync(string filePath) => Task.Run(() => File.OpenRead(filePath));
        public static async Task<ImageSource?> LoadImageFromStream(string filePath)
        {
            if (File.Exists(filePath))
            {
                using FileStream imageStream = await GetImageStreamAsync(filePath).ConfigureAwait(false);
                return ImageSource.FromStream(() => imageStream);
            }
            else return null;
        }
        #endregion
    }
}
