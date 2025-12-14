using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class BooleanToIconConverter : IValueConverter
    {
        public string True { get; set; } = string.Empty;
        public string False { get; set; } = string.Empty;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool trueState)
            {
                if (trueState)
                    return True;
                else 
                    return False;
            }
            return False;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
