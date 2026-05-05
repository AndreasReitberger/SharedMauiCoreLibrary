using AndreasReitberger.Shared.Core.Database.Interfaces;
using AndreasReitberger.Shared.Core.Database.Service;

namespace AndreasReitberger.Shared.Core.Database.Hosting
{
    public static class AppHostBuilderExtensions
    {
        public static MauiAppBuilder WitLocalSQLiteDatabase(this MauiAppBuilder builder, string localPath, string? passPhrase)
        {
            SqliteDatabaseService manager = string.IsNullOrEmpty(passPhrase) ? new(localPath): new(localPath, passPhrase);
            builder.Services.AddSingleton<ISqliteDatabaseService>(manager);
            return builder;
        }
    }
}
