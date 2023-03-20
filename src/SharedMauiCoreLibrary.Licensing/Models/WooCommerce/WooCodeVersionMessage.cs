using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.Shared.Core.Licensing.WooCommerce
{
    /// <summary>
    /// Represents a code version message from Woo Software License
    ///"name": the name
    ///"version": the version
    ///"last_updated": date of last update
    ///"upgrade_notice": notice for this update
    ///"author": the author
    ///"tested": if tested or not
    ///"requires": requires
    ///"homepage": the homepage
    ///"sections": sections => not used
    ///"banners": banner images
    /// </summary>
    public partial class WooCodeVersionMessage : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        [ObservableProperty]
        [JsonProperty("version")]
        string version;

        [ObservableProperty]
        [JsonProperty("last_updated")]
        string lastUpdated;

        [ObservableProperty]
        [JsonProperty("upgrade_notice")]
        string upgradeNotice;

        [ObservableProperty]
        [JsonProperty("author")]
        string author;

        [ObservableProperty]
        [JsonProperty("tested")]
        string tested;

        [ObservableProperty]
        [JsonProperty("requires")]
        string requires;

        [ObservableProperty]
        [JsonProperty("homepage")]
        string homepage;

        [ObservableProperty]
        [JsonProperty("sections")]
        Dictionary<string, string> sections;

        [ObservableProperty]
        [JsonProperty("banners")]
        WooCodeVersionBanners banners;

        #endregion

    }
}
