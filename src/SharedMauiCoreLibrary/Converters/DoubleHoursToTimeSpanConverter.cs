using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class DoubleHoursToTimeSpanConverter : IValueConverter
    {
        public bool RespectMilliSeconds { get; set; } = false;
        /* Translate the name of the accent */
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                TimeSpan ts = TimeSpan.FromHours(System.Convert.ToDouble(value));
                if (!RespectMilliSeconds)
                {
                    ts = new TimeSpan(ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
                }
                return ts;
            }
            catch (Exception)
            {
                return TimeSpan.Zero;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TimeSpan ts)
                return ts.TotalHours;
            else
                return 0;
        }
    }
}
