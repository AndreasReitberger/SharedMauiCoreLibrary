using CommunityToolkit.Mvvm.ComponentModel;

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
        [JsonPropertyName("amount")]
        public partial string Amount { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("sold_At")]
        public partial string SoldAt { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("license")]
        public partial string License { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("support_amount")]
        public partial string SupportAmount { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("supported_until")]
        public partial string SupportedUntil { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("item")]
        public partial EnvatoItem? Item { get; set; }

        [ObservableProperty]
        [JsonPropertyName("code")]
        public partial string PurchaseCode { get; set; } = string.Empty;
        #endregion
    }
}
