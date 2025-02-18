using AndreasReitberger.Shared.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.EventLogger
{
    public partial class AppEvent : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public partial string Message { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string SourceName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial ErrorLevel Level { get; set; } = ErrorLevel.Info;

        [ObservableProperty]
        public partial EventArgs? Args { get; set; }
        #endregion
    }
}
