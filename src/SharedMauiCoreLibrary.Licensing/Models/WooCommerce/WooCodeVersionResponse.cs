using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

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
        [JsonProperty("status")]
        string status = string.Empty;

        [ObservableProperty]
        [JsonProperty("status_code")]
        string errorCode = string.Empty;

        [ObservableProperty]
        [JsonProperty("message")]
        WooCodeVersionMessage? versionMessage;
        #endregion
    }
}
