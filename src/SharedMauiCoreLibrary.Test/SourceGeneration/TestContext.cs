using SharedMauiCoreLibrary.Test.Models;
using System.Text.Json.Serialization;

namespace SharedMauiCoreLibrary.Test.SourceGeneration
{
    [JsonSerializable(typeof(TestModel))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public partial class TestContext : JsonSerializerContext
    {
    }
}
