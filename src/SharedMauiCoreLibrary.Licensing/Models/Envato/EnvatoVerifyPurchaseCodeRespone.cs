using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.Shared.Core.Licensing.Envato
{
    /// <summary>
    /// Represents an envato verify purchase response
    ///"amount": the price of the item
    ///"sold_At": date
    ///"license": "Regular License" | "Extended License"
    ///"support_amount": the price for support
    ///"supported_until": date till support is available
    ///"item": item information
    ///"code": the purchase code
    /// </summary>
    public partial class EnvatoVerifyPurchaseCodeRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("amount")]
        string amount = string.Empty;

        [ObservableProperty]
        [JsonProperty("sold_At")]
        string soldAt = string.Empty;

        [ObservableProperty]
        [JsonProperty("license")]
        string license = string.Empty;

        [ObservableProperty]
        [JsonProperty("support_amount")]
        string supportAmount = string.Empty;

        [ObservableProperty]
        [JsonProperty("supported_until")]
        string supportedUntil = string.Empty;

        [ObservableProperty]
        [JsonProperty("item")]
        EnvatoItem? item;

        [ObservableProperty]
        [JsonProperty("code")]
        string purchaseCode = string.Empty;
        #endregion
    }
}
