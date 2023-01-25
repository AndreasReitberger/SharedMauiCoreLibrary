using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class ColorToBlackWhiteConverter : IValueConverter
    {
        public Color White { get; set; } = Colors.White;
        public Color Black { get; set; } = Colors.Black;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color opposite = Colors.Black;
            if (value is Color color)
            {
                float mean = (color.Red + color.Green + color.Blue) / 3;
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
