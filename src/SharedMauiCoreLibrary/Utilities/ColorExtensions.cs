namespace AndreasReitberger.Shared.Core.Utilities
{

    public static class ColorExtensions
    {
        /// <summary>
        /// This function tints or shades a color by the provided factor.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="factor">The factor for shading / tinting the color. 
        /// 0.1 lights up the color by 10%, -0.1 darkens the color by 10%.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public static Color ChangeColorBrightness(this Color color, float factor)
        {
            float red = color.Red;
            float green = color.Green;
            float blue = color.Blue;
            // Based on calculation example: https://github.com/edelstone/tints-and-shades
            if (factor > 0)
            {
                red += (1 - red) * factor;
                green += (1 - green) * factor;
                blue += (1 - blue) * factor;
            }
            else
            {
                red *= (1 + factor);
                green *= (1 + factor);
                blue *= (1 + factor);
            }

            return Color.FromRgba(red, green, blue, color.Alpha);
        }

        /// <summary>
        /// Ligthens up a color by the provided factor
        /// </summary>
        /// <param name="color">Color to be lighten up</param>
        /// <param name="factor">The factor (must be between 0-1</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public static Color Tint(this Color color, float factor) => ChangeColorBrightness(color, factor);
        /// <summary>
        /// Darkens down a color by the provided factor
        /// </summary>
        /// <param name="color">Color to be darken down</param>
        /// <param name="factor">The factor (must be between 0-1</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public static Color Shade(this Color color, float factor) => ChangeColorBrightness(color, -factor);
    }
}
