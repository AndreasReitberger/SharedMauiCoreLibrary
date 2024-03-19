using AndreasReitberger.Shared.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.EventLogger
{
    public partial class AppErrorEvent : AppEvent
    {
        #region Properties
        [ObservableProperty]
        Exception? exceptio;

        [ObservableProperty]
        int type = 0;
        #endregion

        #region Constructor
        public AppErrorEvent()
        {
            Level = ErrorLevel.Critical;
        }
        #endregion
    }
}
