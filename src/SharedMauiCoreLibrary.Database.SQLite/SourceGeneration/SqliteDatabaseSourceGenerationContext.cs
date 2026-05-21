using AndreasReitberger.Shared.Core.Database.Service;
using AndreasReitberger.Shared.Core.SourceGeneration;

namespace AndreasReitberger.Shared.Core.Database.SourceGeneration
{
    [JsonSerializable(typeof(SqliteDatabaseService))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public partial class SqliteDatabaseSourceGenerationContext : MauiSourceGenerationContext { }

}
