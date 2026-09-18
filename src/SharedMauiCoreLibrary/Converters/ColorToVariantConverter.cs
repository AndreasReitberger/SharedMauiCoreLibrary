using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class ColorToVariantConverter : IValueConverter
    {
        public float Factor { get; set; } = 0.50f;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Color opposite = Colors.Black;
            if (value is Color color)
            {
                float red = color.Red * Factor;
                float green = color.Green * Factor;
                float blue = color.Blue * Factor;
                return new Color(red, green, blue, color.Alpha);
            }
            return opposite;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
