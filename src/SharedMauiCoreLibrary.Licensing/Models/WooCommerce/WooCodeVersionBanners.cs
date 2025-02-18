using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.Shared.Core.Licensing.WooCommerce
{
    public partial class WooCodeVersionBanners : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("low")]
        public partial string LowQualityBanner { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("high")]
        public partial string HighQualityBanner { get; set; } = string.Empty;
        #endregion
    }
}
