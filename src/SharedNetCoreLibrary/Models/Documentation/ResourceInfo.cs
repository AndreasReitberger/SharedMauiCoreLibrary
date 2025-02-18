using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public partial class ResourceInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public partial string Resource { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string ResourceUrl { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Description { get; set; } = string.Empty;
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
