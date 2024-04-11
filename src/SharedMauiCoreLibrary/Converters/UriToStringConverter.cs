using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class UriToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not Uri uri) return string.Empty;
            if (uri is not null)
            {
                return uri.OriginalString;
            }
            else return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
