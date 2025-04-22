using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using Color = Microsoft.Maui.Graphics.Color;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class PlatformThemeService
    {
        //AppWindow m_AppWindow;
        public static partial void SetStatusBarColor(Color color)
        {
            // https://learn.microsoft.com/en-us/windows/apps/develop/title-bar?tabs=wasdk
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                // Not supported on WinUI3
                /* 
                if (m_AppWindow == null)
                {
                    m_AppWindow = GetAppWindowForCurrentWindow();
                }
                AppWindowTitleBar titleBar = m_AppWindow.TitleBar;
                titleBar.BackgroundColor = color.ToWindowsColor();
                */
            }
        }

        #region Private
        AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(wndId);
        }
        #endregion
    }
}
