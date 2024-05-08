using AndreasReitberger.Shared.Core.Utilities;
using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public sealed class HTMLToStringConverter : IValueConverter
    {

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not string html) return string.Empty;
            if (!string.IsNullOrEmpty(html))
            {
                //string decoded = Regex.Replace(html, RegexHelper.HtmlTags, string.Empty);
                string decoded = RegexHelper.HtmlTagsGeneratedRegex().Replace(html, string.Empty);
                return decoded;
            }
            else return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
