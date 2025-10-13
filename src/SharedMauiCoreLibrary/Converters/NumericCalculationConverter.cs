using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class NumericCalculationConverter : IValueConverter
    {
        public double Adjustment { get; set; } = 0;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is double numVal)
            {
                return numVal + Adjustment;
            }
            return value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is double numVal)
            {
                return numVal - Adjustment;
            }
            return value;
        }
    }
}
