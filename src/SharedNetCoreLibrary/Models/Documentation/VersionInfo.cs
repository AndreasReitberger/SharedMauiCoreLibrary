using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public partial class VersionInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        string version = string.Empty;

        [ObservableProperty]
        ObservableCollection<ChangeInfo> changes = [];
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
