using AndreasReitberger.Shared.Core.Localization;
using AndreasReitberger.Shared.Firebase.Events;
using System.Text.Json.Serialization;

namespace AndreasReitberger.Shared.Firebase.SourceGeneration
{
    [JsonSerializable(typeof(CurrentUserChangedEventArgs))]
    [JsonSerializable(typeof(UserDataChangedEventArgs))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public partial class FirebaseSourceGenerationContext : JsonSerializerContext { }
}
