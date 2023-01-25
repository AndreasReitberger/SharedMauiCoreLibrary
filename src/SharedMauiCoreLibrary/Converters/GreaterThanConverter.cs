using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class GreaterThanConverter : IValueConverter
    {
        public double Threshold { get; set; } = 0;
        public bool AllowEqual { get; set; } = true;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double dVal)
            {
                return AllowEqual ? dVal >= Threshold : dVal > Threshold;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
