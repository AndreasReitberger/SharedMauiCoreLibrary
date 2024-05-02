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
            /*
            if (UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) is UIView statusBar && statusBar.RespondsToSelector(
            new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                // change to your desired color 
                statusBar.BackgroundColor = color.ToPlatform();
            }
            */
            // Source: https://blog.verslu.is/maui/change-maui-ios-status-bar-color/
            UIView statusBar;
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                int tag = 4567890;

                UIWindow window = UIApplication.SharedApplication.Delegate.GetWindow();
                statusBar = window.ViewWithTag(tag);
                
                if (statusBar == null || statusBar.Frame != UIApplication.SharedApplication.StatusBarFrame)
                {
                    statusBar = statusBar ?? new(UIApplication.SharedApplication.StatusBarFrame);
                    statusBar.Frame = UIApplication.SharedApplication.StatusBarFrame;
                    statusBar.Tag = tag;

                    window.AddSubview(statusBar);
                }
            }
            else
            {
                if (UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) is UIView view)
                {
                    statusBar = view;
                    if (statusBar != null)
                    {
                        statusBar.BackgroundColor = color.ToPlatform();
                        //statusBar.BackgroundColor = Color.FromArgb("#2B0B98").ToUIColor();
                    }
                }
            }

        }
    }
}
