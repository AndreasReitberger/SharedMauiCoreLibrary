using AndreasReitberger.Shared.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.EventLogger
{
    public partial class AppEvent : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        string message = string.Empty;
        [ObservableProperty]
        string sourceName = string.Empty;

        [ObservableProperty]
        ErrorLevel level = ErrorLevel.Info;

        [ObservableProperty]
        EventArgs? args;
        #endregion
    }
}
