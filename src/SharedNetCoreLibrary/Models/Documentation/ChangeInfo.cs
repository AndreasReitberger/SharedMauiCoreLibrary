using AndreasReitberger.Shared.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public partial class ChangeInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public partial ChangelogType Type { get; set; } = ChangelogType.New;

        [ObservableProperty]
        public partial string Changelog { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string GlyphIcon { get; set; } = string.Empty;
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
