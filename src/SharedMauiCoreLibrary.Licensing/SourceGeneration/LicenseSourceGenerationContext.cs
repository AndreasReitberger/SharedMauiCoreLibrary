using AndreasReitberger.Shared.Core.Licensing.Envato;
using AndreasReitberger.Shared.Core.Licensing.Events;
using AndreasReitberger.Shared.Core.Licensing.WooCommerce;
using AndreasReitberger.Shared.Core.SourceGeneration;

namespace AndreasReitberger.Shared.Core.Licensing.SourceGeneration
{
    [JsonSerializable(typeof(ApplicationVersionResult))]
    [JsonSerializable(typeof(EnvatoActivationResponse))]
    [JsonSerializable(typeof(EnvatoItem))]
    [JsonSerializable(typeof(EnvatoVerifyPurchaseCodeRespone))]
    [JsonSerializable(typeof(LicenseInfo))]
    [JsonSerializable(typeof(LicenseChangedEventArgs))]
    [JsonSerializable(typeof(LicenseErrorEventArgs))]
    [JsonSerializable(typeof(LicenseOptions))]
    [JsonSerializable(typeof(LicenseQueryResult))]
    [JsonSerializable(typeof(WooActivationResponse))]
    [JsonSerializable(typeof(WooCodeVersionResponse))]
    [JsonSerializable(typeof(WooCodeVersionMessage))]
    [JsonSerializable(typeof(WooSoftwareLicenseAction))]
    [JsonSerializable(typeof(WooActivationResponse[]))]
    [JsonSerializable(typeof(WooCodeVersionResponse[]))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public partial class LicenseSourceGenerationContext : JsonSerializerContext { }

}
