using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace AndreasReitberger.Shared.Core.Utilities
{
    public static class ToastManager
    {
        #region Methods
        // Docs: https://learn.microsoft.com/de-de/dotnet/communitytoolkit/maui/alerts/toast
        public static async Task ShowToastNotificationAsync(string message, ToastDuration duration = ToastDuration.Short, double fontSize = 14, CancellationTokenSource? cts = default)
        {
            cts ??= new();
            IToast toast = Toast.Make(message, duration, fontSize);
            await toast.Show(cts.Token);
        }
        #endregion
    }
}
