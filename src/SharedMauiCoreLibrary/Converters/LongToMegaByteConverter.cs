using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class LongToMegaByteConverter : IValueConverter
    {

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            long bytes = System.Convert.ToInt64(value);
            double factor = 1048576;
            double megaBytes = (double)Math.Round((double)(bytes / factor), 2);
            return megaBytes;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
