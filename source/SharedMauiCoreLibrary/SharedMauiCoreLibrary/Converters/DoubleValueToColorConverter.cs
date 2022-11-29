using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public class DoubleValueToColorConverter : BindableObject, IValueConverter, IMarkupExtension<DoubleValueToColorConverter>
    {
        #region Fields
        public static readonly BindableProperty OnZeroProperty =
            BindableProperty.Create(nameof(OnZero), typeof(Color), typeof(DoubleValueToColorConverter), Colors.Gray, BindingMode.TwoWay, null, null);

        public static readonly BindableProperty OnPositiveProperty =
            BindableProperty.Create(nameof(OnPositive), typeof(Color), typeof(DoubleValueToColorConverter), Colors.Green, BindingMode.TwoWay, null, null);

        public static readonly BindableProperty OnNegativeProperty =
            BindableProperty.Create(nameof(OnNegative), typeof(Color), typeof(DoubleValueToColorConverter), Colors.Red, BindingMode.TwoWay, null, null);

        #endregion

        public Color OnZero { get; set; } = Colors.Gray;
        public Color OnPositive { get; set; } = Colors.Green;
        public Color OnNegative { get; set; } = Colors.Red;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not double doubleValue || targetType != typeof(Color)) return null;
            return doubleValue switch
            {
                > 0 => OnPositive,
                < 0 => OnNegative,
                _ => OnZero,
            };
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        DoubleValueToColorConverter IMarkupExtension<DoubleValueToColorConverter>.ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
