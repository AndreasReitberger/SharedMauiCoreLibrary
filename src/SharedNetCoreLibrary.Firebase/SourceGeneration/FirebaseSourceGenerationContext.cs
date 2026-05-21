using AndreasReitberger.Shared.Core.SourceGeneration;
using AndreasReitberger.Shared.Firebase;
using AndreasReitberger.Shared.Firebase.Events;
using System.Text.Json.Serialization;

namespace AndreasReitberger.Shared.Core.Licensing.SourceGeneration
{
    [JsonSerializable(typeof(FirebaseHandler))]
    [JsonSerializable(typeof(CurrentUserChangedEventArgs))]
    [JsonSerializable(typeof(UserDataChangedEventArgs))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public partial class FirebaseSourceGenerationContext : CoreSourceGenerationContext { }

}
