using Foundation;
using Microsoft.Maui.Platform;
using UIKit;
using Color = Microsoft.Maui.Graphics.Color;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class PlatformThemeService
    {
        // Source: https://stackoverflow.com/a/39164921/10083577
        public static partial void SetStatusBarColor(Color color)
        {
            if (OperatingSystem.IsIOSVersionAtLeast(13))
            {
                //var currentWindow = WindowStateManager.Default.GetCurrentUIWindow();
                // Nothing to do here, if needed use the `StatusBarBehavior`
                // Docs: https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/behaviors/statusbar-behavior?tabs=ios
                // Git : https://github.com/CommunityToolkit/Maui/blob/main/src/CommunityToolkit.Maui/Behaviors/PlatformBehaviors/StatusBar/StatusBarBehavior.shared.cs
            }
            else
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
}
