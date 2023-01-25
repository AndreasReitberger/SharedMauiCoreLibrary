using AndreasReitberger.Shared.Core.Enums;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public class ChangelogInfo
    {
        #region Properties
        public ChangelogType Type { get; set; }
        public string Changelog { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string GlyphIcon { get; set; } = string.Empty;
        #endregion

        #region Constructor
        public ChangelogInfo(string version, string changelog, ChangelogType type = ChangelogType.New)
        {
            Version = version;
            Changelog = changelog;
            Type = type;
        }
        #endregion
    }
}
