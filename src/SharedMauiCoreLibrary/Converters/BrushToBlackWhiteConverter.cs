using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class BrushToBlackWhiteConverter : IValueConverter
    {
        public Color White { get; set; } = Colors.White;
        public Color Black { get; set; } = Colors.Black;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color opposite = Colors.Black;
            if (value is SolidColorBrush color)
            {
                float mean = ((color.Color?.Red + color.Color?.Green + color.Color?.Blue) ?? 0) / 3;
                opposite = mean < 0.5 ?
                    White : Black;
            }
            return opposite;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
