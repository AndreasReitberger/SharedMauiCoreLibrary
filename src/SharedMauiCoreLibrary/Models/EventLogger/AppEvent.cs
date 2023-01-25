using AndreasReitberger.Shared.Core.Enums;

namespace AndreasReitberger.Shared.Core.EventLogger
{
    public partial class AppEvent
    {
        #region Properties
        public string Message { get; set; }
        public string SourceName { get; set; }
        public ErrorLevel Level { get; set; }
        public EventArgs Args { get; set; }
        #endregion
    }
}
