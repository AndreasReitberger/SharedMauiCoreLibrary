using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class NullToBooleanVisibilityConverter : IValueConverter
    {

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is not null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
