using AndreasReitberger.Shared.Core.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Theme
{
    public partial class ThemeColorInfo : ObservableObject
    {
        #region Properties
        /// <summary>
        /// A matching name for the theme information.
        /// </summary>
        [ObservableProperty]
        string themeName = string.Empty;

        [ObservableProperty]
        bool isAppDefault = false;

        /// <summary>
        /// The primary color for this theme info. The ligther and darker color will be set automatically 
        /// based on this color and the set `Factor`.
        /// </summary>
        [ObservableProperty]
        Color? primaryColor;
        partial void OnPrimaryColorChanged(Color? value) => UpdatePrimaryColorDependencies(value);

        /// <summary>
        /// A secondary color as addition to the Primary Color. 
        /// </summary>
        [ObservableProperty]
        Color? secondaryColor;

        /// <summary>
        /// This factor will be used to ligthen / darken the primary color. This value should be between 0.1f and 1f
        /// </summary>
        [ObservableProperty]
        float factor = 0.1f;
        partial void OnFactorChanged(float value) => UpdatePrimaryColorDependencies(PrimaryColor);

        // https://github.com/CommunityToolkit/dotnet/issues/555
        // Not yet supported
        [ObservableProperty]
        Color? primaryLigtherColor;

        [ObservableProperty]
        Color? primaryDarkerColor;
        #endregion

        #region Ctor
        public ThemeColorInfo() { }
        public ThemeColorInfo(string themeName, Color primaryColor, Color secondaryColor)
        {
            ThemeName = themeName;
            PrimaryColor = primaryColor;
            SecondaryColor = secondaryColor;
        }
        #endregion

        #region Methods

        void UpdatePrimaryColorDependencies(Color? primaryColor)
        {
            if (primaryColor is not null)
            {
                PrimaryLigtherColor = ColorExtensions.Tint(primaryColor, Factor);
                PrimaryDarkerColor = ColorExtensions.Shade(primaryColor, Factor);
            }
        }

        #endregion
    }
}
