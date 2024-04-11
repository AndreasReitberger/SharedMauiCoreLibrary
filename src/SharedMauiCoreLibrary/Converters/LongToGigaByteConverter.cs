using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class LongToGigaByteConverter : IValueConverter
    {

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            long bytes = System.Convert.ToInt64(value);
            double factor = 1073741824;
            double gigaBytes = (double)Math.Round((double)(bytes / factor), 2);
            return gigaBytes;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
