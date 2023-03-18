using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.Shared.Core.Licensing.WooCommerce
{
    public partial class WooCodeVersionBanners : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("low")]
        string lowQualityBanner;

        [ObservableProperty]
        [JsonProperty("high")]
        string highQualityBanner;
        #endregion
    }
}
