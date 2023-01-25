using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Colors.Black;
            if (value is not string str || string.IsNullOrEmpty(str))
            {
                return color;
            }

            try
            {
                color = Color.FromArgb(str[0] != '#' ? $"#{str}" : str);
            }
            catch (Exception)
            {
                return color;
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
