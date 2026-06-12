using AndreasReitberger.Shared.Core.Database.Service;

namespace AndreasReitberger.Shared.Core.Database.SourceGeneration
{
    [JsonSerializable(typeof(SqliteDatabaseService))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public partial class SqliteDatabaseSourceGenerationContext : JsonSerializerContext { }

}
