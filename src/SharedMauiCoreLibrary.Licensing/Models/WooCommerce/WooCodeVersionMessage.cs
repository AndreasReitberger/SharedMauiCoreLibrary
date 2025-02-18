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
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("version")]
        public partial string Version { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("last_updated")]
        public partial string LastUpdated { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("upgrade_notice")]
        public partial string UpgradeNotice { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("author")]
        public partial string Author { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("tested")]
        public partial string Tested { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("requires")]
        public partial string Requires { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("homepage")]
        public partial string Homepage { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("sections")]
        public partial Dictionary<string, string> Sections { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("banners")]
        public partial WooCodeVersionBanners? Banners { get; set; }

        #endregion

    }
}
