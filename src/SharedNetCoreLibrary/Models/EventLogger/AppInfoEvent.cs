﻿using AndreasReitberger.Shared.Core.Enums;

namespace AndreasReitberger.Shared.Core.EventLogger
{
    public partial class AppInfoEvent : AppEvent
    {
        #region Constructor
        public AppInfoEvent()
        {
            Level = ErrorLevel.Info;
        }
        #endregion
    }
}
