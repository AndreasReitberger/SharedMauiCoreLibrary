using AndreasReitberger.Shared.Core.Events;
using AndreasReitberger.Shared.Core.Localization;

namespace AndreasReitberger.Shared.Core.SourceGeneration
{
    [JsonSerializable(typeof(LanguageChangedEventArgs))]
    [JsonSerializable(typeof(LocalizationInfo))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public partial class CoreSourceGenerationContext : JsonSerializerContext { }
   
}