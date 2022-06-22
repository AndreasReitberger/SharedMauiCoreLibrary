using AndreasReitberger.Shared.Core.Enums;
using AndreasReitberger.Shared.Core.EventLogger;

namespace AndreasReitberger.Shared.Core.Models
{
    public class WatchAppError : AppErrorEvent
    {
        #region Properties
        public WatchAppDevice Device { get; set; } = WatchAppDevice.Unkown;
        public bool SessionValid { get; set; } = false;
        #endregion
    }
}
