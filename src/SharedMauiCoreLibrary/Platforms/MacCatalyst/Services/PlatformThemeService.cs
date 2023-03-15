using Foundation;
using Microsoft.Maui.Platform;
using UIKit;
using Color = Microsoft.Maui.Graphics.Color;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class PlatformThemeService
    {
        // Source: https://stackoverflow.com/a/39164921/10083577
        public partial void SetStatusBarColor(Color color)
        {
            if (UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) is UIView statusBar && statusBar.RespondsToSelector(
            new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                // change to your desired color 
                statusBar.BackgroundColor = color.ToPlatform();
            }
        }
    }
}
