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
        #endregion
    }
}
