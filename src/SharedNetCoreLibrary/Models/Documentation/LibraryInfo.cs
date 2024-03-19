using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public partial class LibraryInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        string library = string.Empty;

        [ObservableProperty]
        string libraryUrl = string.Empty;

        [ObservableProperty]
        string description = string.Empty;

        [ObservableProperty]
        bool stateChanged = false;

        [ObservableProperty]
        string license = string.Empty;

        [ObservableProperty]
        string licenseUrl = string.Empty;
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
