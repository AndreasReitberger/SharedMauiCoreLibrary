using CoreGraphics;
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
            // Source: https://blog.verslu.is/maui/change-maui-ios-status-bar-color/
            UIView statusBar;
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                int tag = 4567890;

                UIWindow window = UIApplication.SharedApplication.Delegate.GetWindow();
                statusBar = window.ViewWithTag(tag);
                //if (statusBar == null || statusBar.Frame != UIApplication.SharedApplication.StatusBarFrame)
                if (statusBar == null || statusBar.Frame != window.WindowScene?.StatusBarManager?.StatusBarFrame)
                {
                    if (window.WindowScene?.StatusBarManager?.StatusBarFrame is CGRect frame)
                    {
                        statusBar ??= new(frame);
                        statusBar.Frame = frame;
                        statusBar.Tag = tag;
                        window.AddSubview(statusBar);
                    }
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
