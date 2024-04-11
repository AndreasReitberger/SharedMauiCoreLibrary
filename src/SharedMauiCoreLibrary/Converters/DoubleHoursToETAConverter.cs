using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class DoubleHoursToETAConverter : IValueConverter
    {
        public bool RespectMilliSeconds { get; set; } = false;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                TimeSpan ts = TimeSpan.FromHours(System.Convert.ToDouble(value));
                // Ignore ms for better displaying
                if (!RespectMilliSeconds)
                {
                    ts = new TimeSpan(ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
                }
                DateTime eta = DateTime.Now.Add(ts);
                return eta;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime eta)
            {
                TimeSpan time = eta.Subtract(DateTime.Now);
                return time.TotalHours;
            }
            else return 0;
        }
    }
}
