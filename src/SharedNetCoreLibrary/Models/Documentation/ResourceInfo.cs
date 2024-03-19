using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public partial class ResourceInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        string resource = string.Empty;

        [ObservableProperty]
        string resourceUrl = string.Empty;

        [ObservableProperty]
        string description = string.Empty;
        #endregion

        #region Constructor
        public ResourceInfo(string resource, string resourceUrl, string description)
        {
            Resource = resource;
            ResourceUrl = resourceUrl;
            Description = description;
        }
        #endregion
    }
}
