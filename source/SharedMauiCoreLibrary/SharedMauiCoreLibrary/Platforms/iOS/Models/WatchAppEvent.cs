using AndreasReitberger.Shared.Core.Enums;
using AndreasReitberger.Shared.Core.EventLogger;

namespace AndreasReitberger.Shared.Core.Models
{
    public class WatchAppEvent : AppEvent
    {
        #region Properties
        public WatchAppDevice Device { get; set; } = WatchAppDevice.Unkown;
        public bool SessionValid { get; set; } = false;
        #endregion
    }
}
