using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class NullOrEmptyToBooleanVisibilityConverter : IValueConverter
    {

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not null)
                return true;
            else if (value is Array array)
            {
                return array.Length == 0;
            }
            else return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
