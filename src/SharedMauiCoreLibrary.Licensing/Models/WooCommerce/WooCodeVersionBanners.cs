using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.Shared.Core.Licensing.WooCommerce
{
    public partial class WooCodeVersionBanners : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("low")]
        string lowQualityBanner = string.Empty;

        [ObservableProperty]
        [JsonProperty("high")]
        string highQualityBanner = string.Empty;
        #endregion
    }
}
