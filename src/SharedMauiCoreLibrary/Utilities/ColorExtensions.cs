namespace AndreasReitberger.Shared.Core.Utilities
{
    // Source: https://gist.github.com/JoshClose/1367327
    // Author: https://gist.github.com/JoshClose
    public static class ColorExtensions
    {

        /// <summary>
        /// Tints the color by the given percent.
        /// </summary>
        /// <param name="color">The color being tinted.</param>
        /// <param name="percent">The percent to tint. Ex: 0.1 will make the color 10% lighter.</param>
        /// <returns>The new tinted color.</returns>
        public static Color Lighten(this Color color, float percent)
        {
            float lighting = color.GetLuminosity();
            lighting += lighting * percent;
            if (lighting > 1.0)
            {
                lighting = 1;
            }
            else if (lighting <= 0)
            {
                lighting = 0.1f;
            }
            Color tintedColor = ColorHelper.FromHsl((int)color.Alpha, color.GetHue(), color.GetSaturation(), lighting);

            return tintedColor;
        }

        /// <summary>
        /// Tints the color by the given percent.
        /// </summary>
        /// <param name="color">The color being tinted.</param>
        /// <param name="percent">The percent to tint. Ex: 0.1 will make the color 10% darker.</param>
        /// <returns>The new tinted color.</returns>
        public static Color Darken(this Color color, float percent)
        {
            float lighting = color.GetLuminosity();
            lighting -= lighting * percent;
            if (lighting > 1.0)
            {
                lighting = 1;
            }
            else if (lighting <= 0)
            {
                lighting = 0;
            }
            Color tintedColor = ColorHelper.FromHsl((int)color.Alpha, color.GetHue(), color.GetSaturation(), lighting);

            return tintedColor;
        }
    }
}
