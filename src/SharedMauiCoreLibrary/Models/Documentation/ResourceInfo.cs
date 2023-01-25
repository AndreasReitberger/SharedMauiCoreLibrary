namespace AndreasReitberger.Shared.Core.Documentation
{
    public class ResourceInfo
    {
        #region Properties
        public string Resource { get; set; }
        public string ResourceUrl { get; set; }
        public string Description { get; set; }
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
