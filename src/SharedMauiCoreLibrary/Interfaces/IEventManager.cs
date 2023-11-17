using AndreasReitberger.Shared.Core.EventLogger;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IEventManager
    {
        #region Properties
#nullable enable
        IDispatcher? Dispatcher { get; set; }
#nullable disable
        bool AllowCrashAnalyticsData { get; set; }
        bool AllowAnalyticsData { get; set; }
        bool HasCriticalError { get; set; }

        public static ObservableCollection<AppEvent> Events { get; }
        #endregion

        #region Methods
#nullable enable
        void LogError(Exception exception, bool forceReport = false, IDispatcher? dispatcher = null);
        void LogError(AppErrorEvent error, bool forceReport = false, IDispatcher? dispatcher = null);
        void LogInfo(AppInfoEvent info, bool forceReport = false, IDispatcher? dispatcher = null);
        void LogWarning(AppWarningEvent warning, bool forceReport = false, IDispatcher? dispatcher = null);
        void LogEvent(AppEvent appEvent, bool forceReport = false, IDispatcher? dispatcher = null);
        void Clear(IDispatcher? dispatcher = null);
#nullable disable
        #endregion
    }
}
