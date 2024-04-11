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
        string name = string.Empty;

        [ObservableProperty]
        [JsonProperty("version")]
        string version = string.Empty;

        [ObservableProperty]
        [JsonProperty("last_updated")]
        string lastUpdated = string.Empty;

        [ObservableProperty]
        [JsonProperty("upgrade_notice")]
        string upgradeNotice = string.Empty;

        [ObservableProperty]
        [JsonProperty("author")]
        string author = string.Empty;

        [ObservableProperty]
        [JsonProperty("tested")]
        string tested = string.Empty;

        [ObservableProperty]
        [JsonProperty("requires")]
        string requires = string.Empty;

        [ObservableProperty]
        [JsonProperty("homepage")]
        string homepage = string.Empty;

        [ObservableProperty]
        [JsonProperty("sections")]
        Dictionary<string, string> sections = [];

        [ObservableProperty]
        [JsonProperty("banners")]
        WooCodeVersionBanners? banners;

        #endregion

    }
}
