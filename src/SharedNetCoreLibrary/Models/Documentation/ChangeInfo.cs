using AndreasReitberger.Shared.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public partial class ChangeInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        ChangelogType type = ChangelogType.New;

        [ObservableProperty]
        string changelog = string.Empty;
        [ObservableProperty]
        string glyphIcon = string.Empty;
        #endregion

        #region Constructor
        public ChangeInfo() { }
        public ChangeInfo(string changelog, ChangelogType type = ChangelogType.New)
        {
            Changelog = changelog;
            Type = type;
        }
        #endregion
    }
}
