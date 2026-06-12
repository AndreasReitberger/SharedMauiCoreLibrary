using AndreasReitberger.Shared.Firebase.Events;
using System.Text.Json.Serialization;

namespace AndreasReitberger.Shared.Firebase.SourceGeneration
{
    [JsonSerializable(typeof(FirebaseHandler))]
    [JsonSerializable(typeof(CurrentUserChangedEventArgs))]
    [JsonSerializable(typeof(UserDataChangedEventArgs))]
    [JsonSourceGenerationOptions(WriteIndented = true, GenerationMode = JsonSourceGenerationMode.Metadata)]
    public partial class FirebaseSourceGenerationContext : JsonSerializerContext // CoreSourceGenerationContext
    { }

}
