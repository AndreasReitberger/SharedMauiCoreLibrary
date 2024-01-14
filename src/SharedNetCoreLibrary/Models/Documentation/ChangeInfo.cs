using AndreasReitberger.Shared.Core.Enums;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public class ChangeInfo
    {
        #region Properties
        public ChangelogType Type { get; set; }
        public string Changelog { get; set; } = string.Empty;
        public string GlyphIcon { get; set; } = string.Empty;
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
