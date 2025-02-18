using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public partial class LibraryInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public partial string Library { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string LibraryUrl { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool StateChanged { get; set; } = false;

        [ObservableProperty]
        public partial string License { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string LicenseUrl { get; set; } = string.Empty;
        #endregion

        #region Constructor
        public LibraryInfo() { }
        public LibraryInfo(string library, string libraryUrl, string description, string license, string licenseUrl, bool stateChanged = false)
        {
            Library = library;
            LibraryUrl = libraryUrl;
            Description = description;
            License = license;
            LicenseUrl = licenseUrl;
            StateChanged = stateChanged;
        }
        #endregion
    }
}
