using AndreasReitberger.Shared.Core.Database.Interfaces;
using AndreasReitberger.Shared.Core.Database.Service;

namespace AndreasReitberger.Shared.Core.Database.Hosting
{
    public static class AppHostBuilderExtensions
    {
        public static MauiAppBuilder WitLocalSQLiteDatabase(this MauiAppBuilder builder, string connectionString)
        {
            SqlDatabaseService manager = new(connectionString);
            builder.Services.AddSingleton<ISqlDatabaseService>(manager);
            return builder;
        }
    }
}
