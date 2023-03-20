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
        string id;

        [ObservableProperty]
        [JsonProperty("name")]
        string itemName;

        [ObservableProperty]
        [JsonProperty("number_of_sales")]
        string sales;

        [ObservableProperty]
        [JsonProperty("author_username")]
        string author;

        [ObservableProperty]
        [JsonProperty("author_url")]
        string authorUrl;

        [ObservableProperty]
        [JsonProperty("url")]
        string url;

        [ObservableProperty]
        [JsonProperty("updatedAt")]
        string updatedAt;
        #endregion
    }
}
