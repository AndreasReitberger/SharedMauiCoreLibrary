using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not byte[] imageAsBytes)
            {
                return null;
            }
            ImageSource image = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
