using CoreGraphics;
using System.Drawing;
using UIKit;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class ImageResizerService
    {
        // Source: https://github.com/xamarin/xamarin-forms-samples/blob/master/XamFormsImageResize/XamFormsImageResize/ImageResizer.cs
        public partial byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            UIImage? originalImage = ImageFromByteArray(imageData);
            if (originalImage is null) return [];

            UIImageOrientation orientation = originalImage.Orientation;
            bool landscape = originalImage.Size.Width >= originalImage.Size.Height;
            float ratio = Convert.ToSingle(originalImage.Size.Width / originalImage.Size.Height);

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
            //create a 24bit RGB image
            using CGBitmapContext context = new(IntPtr.Zero,
                                                 (int)width, (int)height, 8,
                                                 4 * (int)width, CGColorSpace.CreateDeviceRGB(),
                                                 CGImageAlphaInfo.PremultipliedFirst);
            RectangleF imageRect = new(0, 0, width, height);

            // draw the image
            context.DrawImage(imageRect, originalImage.CGImage);
            if (context?.ToImage() is CGImage gimage)
            {
                UIImage? resizedImage = UIImage.FromImage(gimage, 0, orientation);
                // save the image as a jpeg
                return resizedImage?.AsJPEG()?.ToArray() ?? [];
            }
            return [];
        }

        UIImage? ImageFromByteArray(byte[] data)
        {
            if (data is null)
            {
                return null;
            }
            UIImage image;
            try
            {
                image = new UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Image load failed: " + e.Message);
                return null;
            }
            return image;
        }
    }
}
