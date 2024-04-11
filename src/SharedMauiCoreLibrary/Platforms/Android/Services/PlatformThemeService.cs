using Android.OS;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Color = Microsoft.Maui.Graphics.Color;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class PlatformThemeService
    {
        // Source: https://stackoverflow.com/a/39164921/10083577
        public partial void SetStatusBarColor(Color color)
        {
            // The SetStatusBarcolor is new since API 21
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var androidColor = color.AddLuminosity(-0.1f).ToAndroid();
                Microsoft.Maui.ApplicationModel.Platform.CurrentActivity?.Window?.SetStatusBarColor(androidColor);
            }
            else
            {
                // Here you will just have to set your 
                // color in styles.xml file as shown below.
            }
        }
    }
}
