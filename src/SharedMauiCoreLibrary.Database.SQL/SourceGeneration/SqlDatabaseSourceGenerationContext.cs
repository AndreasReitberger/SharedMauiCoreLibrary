using AndreasReitberger.Shared.Core.Database.Service;
using AndreasReitberger.Shared.Core.SourceGeneration;

namespace AndreasReitberger.Shared.Core.Database.SourceGeneration
{
    [JsonSerializable(typeof(SqlDatabaseService))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public partial class SqlDatabaseSourceGenerationContext : MauiSourceGenerationContext { }

}
