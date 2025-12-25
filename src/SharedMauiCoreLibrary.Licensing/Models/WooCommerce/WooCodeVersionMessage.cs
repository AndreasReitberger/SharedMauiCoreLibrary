using CommunityToolkit.Mvvm.ComponentModel;

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
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("version")]
        public partial string Version { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("last_updated")]
        public partial string LastUpdated { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("upgrade_notice")]
        public partial string UpgradeNotice { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("author")]
        public partial string Author { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("tested")]
        public partial string Tested { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("requires")]
        public partial string Requires { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("homepage")]
        public partial string Homepage { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("sections")]
        public partial Dictionary<string, string> Sections { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("banners")]
        public partial WooCodeVersionBanners? Banners { get; set; }

        #endregion

    }
}
