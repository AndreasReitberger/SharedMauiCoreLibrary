using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing.WooCommerce
{
    public partial class WooCodeVersionBanners : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("low")]
        public partial string LowQualityBanner { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("high")]
        public partial string HighQualityBanner { get; set; } = string.Empty;
        #endregion
    }
}
