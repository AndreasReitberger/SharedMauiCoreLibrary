using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    /// <summary>
    /// Returns true, if the passed Color needs a light foreground
    /// </summary>
    public sealed class ColorToLightForgroundConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                float mean = (color.Red + color.Green + color.Blue) / 3;
                return mean < 0.5;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
