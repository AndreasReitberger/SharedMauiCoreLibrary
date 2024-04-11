using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public class StringToUpercaseConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                return str?.ToUpper();
            }
            else return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
