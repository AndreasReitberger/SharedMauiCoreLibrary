using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class DoubleSecondsToTimeSpanConverter : IValueConverter
    {
        public bool RespectMilliSeconds { get; set; } = false;
        /* Translate the name of the accent */
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                TimeSpan ts = TimeSpan.FromSeconds(System.Convert.ToDouble(value));
                if(!RespectMilliSeconds)
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan ts = (TimeSpan)value;
            if (ts == null)
                return 0;
            return ts.TotalSeconds;
        }
    }
}
