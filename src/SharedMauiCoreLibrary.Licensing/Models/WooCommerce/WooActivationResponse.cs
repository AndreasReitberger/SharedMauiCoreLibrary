using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing.WooCommerce
{
    /// <summary>
    /// Represents an activation response from Woo Software License
    ///"status": "error" | "success"
    ///"status_code": s100 | s101.... => https://woosoftwarelicense.com/documentation/explain-api-status-codes/
    ///"message": "Message"
    /// </summary>
    public partial class WooActivationResponse : ObservableObject, IActivationResponse
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
        public partial string ErrorMessage { get; set; } = string.Empty;
        #endregion
    }
}
