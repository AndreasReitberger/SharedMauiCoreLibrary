using AndreasReitberger.Shared.Core.Enums;
using System.Globalization;

namespace AndreasReitberger.Shared.Core.Converters
{
    public class StringToChangelogIconConverter : IValueConverter
    {

        public const string Bug = "\U000f00e4";
        public const string CogRefreshOutline = "\U000f145f";
        public const string Autorenew = "\U000f006a";
        public const string PlaylistPlus = "\U000f0412";

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            _ = Enum.TryParse(value?.ToString(), out ChangelogType type);
            string glyp = type switch
            {
                ChangelogType.New => PlaylistPlus,
                ChangelogType.BugFix => Bug,
                ChangelogType.Changed => Autorenew,
                ChangelogType.Updated => CogRefreshOutline,
                _ => PlaylistPlus,
            };
            return glyp;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
