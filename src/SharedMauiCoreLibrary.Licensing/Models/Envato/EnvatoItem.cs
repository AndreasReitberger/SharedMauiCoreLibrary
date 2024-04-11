using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

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
        [JsonProperty("id")]
        string id = string.Empty;

        [ObservableProperty]
        [JsonProperty("name")]
        string itemName = string.Empty;

        [ObservableProperty]
        [JsonProperty("number_of_sales")]
        string sales = string.Empty;

        [ObservableProperty]
        [JsonProperty("author_username")]
        string author = string.Empty;

        [ObservableProperty]
        [JsonProperty("author_url")]
        string authorUrl = string.Empty;

        [ObservableProperty]
        [JsonProperty("url")]
        string url = string.Empty;

        [ObservableProperty]
        [JsonProperty("updatedAt")]
        string updatedAt = string.Empty;
        #endregion
    }
}
