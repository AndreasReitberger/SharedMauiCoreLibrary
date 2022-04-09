using AndreasReitberger.Shared.Core.Enums;

namespace AndreasReitberger.Shared.Core.EventLogger
{
    public class AppInfoEvent : AppEvent
    {
        #region Properties

        #endregion

        #region Constructor
        public AppInfoEvent()
        {
            Level = ErrorLevel.Info;
        }
        #endregion
    }
}
