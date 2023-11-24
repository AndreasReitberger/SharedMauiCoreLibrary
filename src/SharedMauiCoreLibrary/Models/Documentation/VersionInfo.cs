using System.Collections.ObjectModel;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public class VersionInfo
    {
        #region Properties
        public string Version { get; set; } = string.Empty;
        public ObservableCollection<ChangeInfo> Changes { get; set; } = new();
        #endregion

        #region Constructor
        public VersionInfo() { }
        public VersionInfo(string version, ObservableCollection<ChangeInfo> changes)
        {
            Version = version;
            Changes = changes;
        }
        #endregion
    }
}
