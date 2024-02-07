using AndreasReitberger.Shared.Core.Theme;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Models.Theme
{
    public partial class DefaultThemeManager : ObservableObject//, IThemeManager
    {
        #region Instance
        static DefaultThemeManager? _instance = null;
        static readonly object Lock = new();
        public static DefaultThemeManager? Instance
        {
            get
            {
                lock (Lock)
                {
                    _instance ??= new DefaultThemeManager();
                }
                return _instance;
            }
            set
            {
                if (_instance == value) return;
                lock (Lock)
                {
                    _instance = value;
                }
            }

        }
        #endregion

        #region Properties
        [ObservableProperty]
        List<ThemeColorInfo> availableColors =
        [
            new ThemeColorInfo() { ThemeName = ".NET MAUI", PrimaryColor = Color.FromArgb("#512BD4"), IsAppDefault = true },
            new ThemeColorInfo() { ThemeName = Colors.Gray.ToHex(), PrimaryColor = Colors.Gray },
            new ThemeColorInfo() { ThemeName = Colors.Brown.ToHex(), PrimaryColor = Colors.Brown },
            new ThemeColorInfo() { ThemeName = Colors.Green.ToHex(), PrimaryColor = Colors.Green },
            new ThemeColorInfo() { ThemeName = Colors.Red.ToHex(), PrimaryColor = Colors.Red },
            new ThemeColorInfo() { ThemeName = Colors.Orange.ToHex(), PrimaryColor = Colors.Orange },
            new ThemeColorInfo() { ThemeName = Colors.Blue.ToHex(), PrimaryColor = Colors.Blue },
            new ThemeColorInfo() { ThemeName = Colors.LightGray.ToHex(), PrimaryColor = Colors.LightGray },
            new ThemeColorInfo() { ThemeName = Colors.Teal.ToHex(), PrimaryColor = Colors.Teal },
            new ThemeColorInfo() { ThemeName = Colors.Purple.ToHex(), PrimaryColor = Colors.Purple },
            new ThemeColorInfo() { ThemeName = Colors.Pink.ToHex(), PrimaryColor = Colors.Pink },
            new ThemeColorInfo() { ThemeName = Colors.Beige.ToHex(), PrimaryColor = Colors.Beige },
            new ThemeColorInfo() { ThemeName = Colors.Violet.ToHex(), PrimaryColor = Colors.Violet },
            new ThemeColorInfo() { ThemeName = Colors.Silver.ToHex(), PrimaryColor = Colors.Silver },
            new ThemeColorInfo() { ThemeName = Colors.Gold.ToHex(), PrimaryColor = Colors.Gold },
        ];

        public ThemeColorInfo ActiveTheme => AvailableColors?.FirstOrDefault(themeInfo => themeInfo.IsAppDefault);
        #endregion
    }
}
