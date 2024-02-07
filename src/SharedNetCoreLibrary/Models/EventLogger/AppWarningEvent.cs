using AndreasReitberger.Shared.Core.Enums;

namespace AndreasReitberger.Shared.Core.EventLogger
{
    public class AppWarningEvent : AppEvent
    {
        #region Properties

        #endregion

        #region Constructor
        public AppWarningEvent()
        {
            Level = ErrorLevel.Warning;
        }
        #endregion
    }
}
