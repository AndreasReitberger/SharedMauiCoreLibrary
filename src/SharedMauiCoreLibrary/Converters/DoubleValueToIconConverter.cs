using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public class DoubleValueToIconConverter : IValueConverter, IMarkupExtension<DoubleValueToIconConverter>
    {
        public string OnZero { get; set; } = string.Empty;
        public string OnPositive { get; set; } = string.Empty;
        public string OnNegative { get; set; } = string.Empty;
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not double doubleValue || targetType != typeof(string)) return null;
            return doubleValue switch
            {
                > 0 => OnPositive,
                < 0 => OnNegative,
                _ => OnZero,
            };
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public object? ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        DoubleValueToIconConverter IMarkupExtension<DoubleValueToIconConverter>.ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
