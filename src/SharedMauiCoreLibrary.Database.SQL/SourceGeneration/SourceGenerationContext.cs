using AndreasReitberger.Shared.Core.Database.Service;

namespace AndreasReitberger.Shared.Core.Database.SourceGeneration
{
    [JsonSerializable(typeof(SqlDatabaseService))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public partial class DatabaseSourceGenerationContext : JsonSerializerContext { }

}
