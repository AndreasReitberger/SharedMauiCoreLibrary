using Android.OS;
using Microsoft.Maui.Platform;
using System.Runtime.Versioning;
using Color = Microsoft.Maui.Graphics.Color;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class PlatformThemeService
    {
        // Source: https://stackoverflow.com/a/39164921/10083577
        [UnsupportedOSPlatform("Android35.0")]
        public static partial void SetStatusBarColor(Color color)
        {
            Android.Graphics.Color androidColor = color.AddLuminosity(-0.1f).ToPlatform();
            // The SetStatusBarcolor is new since API 21
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop && Build.VERSION.SdkInt < BuildVersionCodes.VanillaIceCream)
            {
                Platform.CurrentActivity?.Window?.SetStatusBarColor(androidColor);
            }
            else if (Build.VERSION.SdkInt >= BuildVersionCodes.VanillaIceCream)
            {

            }
            else
            {
                // Here you will just have to set your 
                // color in styles.xml file as shown below.
            }
        }
    }
}
