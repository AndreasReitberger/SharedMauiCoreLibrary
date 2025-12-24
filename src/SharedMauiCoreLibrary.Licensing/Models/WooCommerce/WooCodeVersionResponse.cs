using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing.WooCommerce
{
    /// <summary>
    /// Represents a code version response from Woo Software License
    ///"status": "error" | "success"
    ///"status_code": s100 | s101.... => https://woosoftwarelicense.com/documentation/explain-api-status-codes/
    ///"message": "Array of version information"
    /// </summary>
    public partial class WooCodeVersionResponse : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("status")]
        public partial string Status { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("status_code")]
        public partial string ErrorCode { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("message")]
        public partial WooCodeVersionMessage? VersionMessage { get; set; }
        #endregion
    }
}
