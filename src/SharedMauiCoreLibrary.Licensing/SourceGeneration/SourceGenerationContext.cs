using AndreasReitberger.Shared.Core.Licensing.Envato;
using AndreasReitberger.Shared.Core.Licensing.Events;
using AndreasReitberger.Shared.Core.Licensing.WooCommerce;

namespace AndreasReitberger.Shared.Core.Licensing.SourceGeneration
{
    [JsonSerializable(typeof(LicenseErrorEventArgs))]
    [JsonSerializable(typeof(EnvatoVerifyPurchaseCodeRespone))]
    [JsonSerializable(typeof(LicenseChangedEventArgs))]
    [JsonSerializable(typeof(WooActivationResponse[]))]
    [JsonSerializable(typeof(WooCodeVersionResponse[]))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public partial class LicenseSourceGenerationContext : JsonSerializerContext { }

}
