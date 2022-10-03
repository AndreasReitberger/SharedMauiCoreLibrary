using Android.Graphics;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class ImageResizerService
    {
        // Source: https://github.com/xamarin/xamarin-forms-samples/blob/master/XamFormsImageResize/XamFormsImageResize/ImageResizer.cs
        public partial byte[] ResizeImage(byte[] imageData, float width, float height = -1)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);

            bool landscape = originalImage.Width >= originalImage.Height;
            float ratio = Convert.ToSingle((float)originalImage.Width / (float)originalImage.Height);

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
            resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
            return ms.ToArray();
        }
    }
}
