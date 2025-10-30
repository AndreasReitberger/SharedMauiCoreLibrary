using Android.Graphics;
using System.Runtime.Versioning;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class ImageResizerService
    {
        // Source: https://github.com/xamarin/xamarin-forms-samples/blob/master/XamFormsImageResize/XamFormsImageResize/ImageResizer.cs
        [SupportedOSPlatform(SPlatforms.AndroidVersion)]
        public partial byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            // Load the bitmap
            Bitmap? originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            if (originalImage is null) return [];

            bool landscape = originalImage.Width >= originalImage.Height;
            float ratio = Convert.ToSingle(originalImage.Width / originalImage.Height);

            if (height <= -1)
            {
                if (landscape)
                    height = width / ratio;
                else
                    height = width * ratio;
            }
            else if (width <= -1)
            {
                if (landscape)
                    width = height / ratio;
                else
                    width = height * ratio;
            }

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using MemoryStream ms = new();
            if (Bitmap.CompressFormat.Jpeg is Bitmap.CompressFormat format)
                resizedImage.Compress(format: format, 100, ms);
            return ms.ToArray();
        }
    }
}
