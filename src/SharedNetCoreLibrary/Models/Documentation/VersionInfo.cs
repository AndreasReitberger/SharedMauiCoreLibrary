using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public partial class VersionInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public partial string Version { get; set; } = string.Empty;

        [ObservableProperty]
        public partial ObservableCollection<ChangeInfo> Changes { get; set; } = [];
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
