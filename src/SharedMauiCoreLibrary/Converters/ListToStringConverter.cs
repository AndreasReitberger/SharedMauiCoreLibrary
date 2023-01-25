using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class ListToStringConverter : IValueConverter
    {
        public string Separator { get; set; } = Environment.NewLine;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<string> list)
            {
                return string.Join(Separator, list);
            }
            else return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                return new List<string>(str.Split(Separator, StringSplitOptions.RemoveEmptyEntries));
            }
            else return new List<string>();
        }
    }
}
