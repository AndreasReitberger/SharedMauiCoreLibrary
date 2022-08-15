using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class LongToMonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                int month = System.Convert.ToInt32(value);
                if (month > 0 && month <= 12)
                {

                }
                else
                    month = 1;
                string name = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                return name;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
