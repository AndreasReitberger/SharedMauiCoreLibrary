using AndreasReitberger.Shared.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.EventLogger
{
    public partial class AppErrorEvent : AppEvent
    {
        #region Properties
        [ObservableProperty]
        public partial Exception? Exception { get; set; }

        [ObservableProperty]
        public partial int Type { get; set; } = 0;
        #endregion

        #region Constructor
        public AppErrorEvent()
        {
            Level = ErrorLevel.Critical;
        }
        #endregion
    }
}
