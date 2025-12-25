using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing.Envato
{
    /// <summary>
    /// Represents an envato item
    ///"id": the numeric id of the item
    ///"name": item name
    ///"number_of_sales": sales of this item
    ///"author_username": the author username
    ///"author_url": the url to the author page
    ///"url": item url
    ///"updatedAt": last updated date of this item
    /// </summary>
    public partial class EnvatoItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("id")]
        public partial string Id { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string ItemName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("number_of_sales")]
        public partial string Sales { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("author_username")]
        public partial string Author { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("author_url")]
        public partial string AuthorUrl { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("url")]
        public partial string Url { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("updatedAt")]
        public partial string UpdatedAt { get; set; } = string.Empty;
        #endregion
    }
}
