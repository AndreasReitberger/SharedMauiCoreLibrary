using AndreasReitberger.Shared.Core.Database.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace AndreasReitberger.Shared.Core.Database.Service
{
    public partial class SqliteDatabaseService(string databasePath) : ObservableObject, ISqliteDatabaseService
    {
        #region Const
        public const SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache;
        #endregion

        #region Properties

        [ObservableProperty]
        public partial string DatabasePath { get; set; } = databasePath;

        [ObservableProperty]
        public partial string? Passphrase { get; set; }

        [ObservableProperty]
        public partial SQLiteAsyncConnection? Database { get; set; }

        #endregion

        #region Ctor

        public SqliteDatabaseService(string databasePath, string passphrase) : this(databasePath) 
        {
            Passphrase = passphrase;
        }
        #endregion

        #region Methods

        public async Task ConnectAsync()
        {
            await DisconnectAsync().ConfigureAwait(false);
            SQLiteConnectionString connection = 
                string.IsNullOrEmpty(Passphrase) ? 
                new (DatabasePath, openFlags: Flags, storeDateTimeAsTicks: true) : 
                new(DatabasePath, openFlags: Flags, storeDateTimeAsTicks: true, key: Passphrase);
            Database = new(connection);
        }

        public async Task DisconnectAsync()
        {
            if (Database is not null)
            {
                await Database.CloseAsync().ConfigureAwait(false);
                Database = null;
            }
        }

        public async Task<CreateTableResult?> CreateTableAsync<T>(CreateFlags flags = CreateFlags.None) where T : new()
        {
            if (Database is not null)
            {
                return await Database.CreateTableAsync<T>(flags).ConfigureAwait(false);
            }
            return null;
        }

        public async Task<CreateTablesResult?> CreateTablesAsync<T>(List<Type> types,CreateFlags flags = CreateFlags.None) where T : new()
        {
            if (Database is not null)
            {
                return await Database.CreateTablesAsync(flags, [.. types]).ConfigureAwait(false);
            }
            return null;
        }

        public async Task<List<T>?> GetAllWithChildrenAsync<T>(bool recursive = true) where T : new()
        {
            if (Database is not null)
            {
                return await Database.GetAllWithChildrenAsync<T>(recursive: recursive).ConfigureAwait(false);
            }
            return null;
        }

        public async Task<T?> GetWithChildrenAsync<T>(object primaryKey, bool recursive = true) where T : new()
        {
            if (Database is not null)
            {
                try
                {
                    return await Database.GetWithChildrenAsync<T>(primaryKey, recursive: recursive).ConfigureAwait(false);
                }
                catch { }
            }
            return default;
        }

        public async Task InsertWithChildrenAsync<T>(T insert, bool replace = false, bool recursive = true) where T : new()
        {
            if (Database is not null)
            {
                if (replace)
                    await Database.InsertOrReplaceWithChildrenAsync(insert, recursive: recursive).ConfigureAwait(false);
                else
                    await Database.InsertWithChildrenAsync(insert, recursive: recursive).ConfigureAwait(false);
            }
        }

        public async Task InsertAllWithChildrenAsync<T>(IList<T> insert, bool replace = false, bool recursive = true) where T : new()
        {
            if (Database is not null)
            {
                if (replace)
                    await Database.InsertOrReplaceAllWithChildrenAsync(insert, recursive: recursive).ConfigureAwait(false);
                else
                    await Database.InsertAllWithChildrenAsync(insert, recursive: recursive).ConfigureAwait(false);
            }
        }
        public async Task<int> DeleteWithChildrenAsync<T>(object primaryKey) where T : new()
        {
            if (Database is not null)
            {
                return await Database.DeleteAsync<T>(primaryKey).ConfigureAwait(false);
            }
            return default;
        }

        public List<TableMapping>? GetTableMappings() => Database?.TableMappings.ToList();
        
        public async Task<List<int>> DropAllTableAsync()
        {
            List<int> ids = [];
            if (Database is not null)
            {
                foreach (TableMapping mapping in Database.TableMappings)
                {
                    ids.Add(await Database.DropTableAsync(mapping).ConfigureAwait(false));
                }
            }
            return ids;
        }

        public async Task<List<int>> TryDropAllTableAsync()
        {
            List<int> ids = [];
            if (Database is not null)
            { 
                foreach (TableMapping mapping in Database.TableMappings)
                {
                    try
                    {
                        ids.Add(await Database.DropTableAsync(mapping).ConfigureAwait(false));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            return ids;
        }

        public async Task<bool> TryDropTableAsync<T>()
        {
            try
            {
                if (Database is null) return false;
                if (Database.TableMappings.FirstOrDefault(m => m.MappedType == typeof(T)) is TableMapping mapping)
                {
                    int result = await Database.DropTableAsync(mapping).ConfigureAwait(false);
                    return result > 0;
                }
            }
            catch (Exception) { }
            return false;
        }

        public async Task<bool> TryDropTableAsync(TableMapping mapping)
        {
            try
            {
                if (Database is null) return false;
                int result = await Database.DropTableAsync(mapping);
                return result > 0;
            }
            catch (Exception) { }
            return false;
        }

        public async Task<List<int>> ClearAllTableAsync()
        {
            List<int> ids = [];
            if (Database is not null)
            {
                foreach (TableMapping mapping in Database.TableMappings)
                {
                    ids.Add(await Database.DeleteAllAsync(mapping).ConfigureAwait(false));
                }
            }
            return ids;
        }

        public Task<int> ClearTableAsync(TableMapping mapping) => Database?.DeleteAllAsync(mapping);
        
        public async Task<List<int>> TryClearAllTableAsync()
        {
            List<int> ids = [];
            if (Database is not null)
            {
                foreach (TableMapping mapping in Database.TableMappings)
                {
                    try
                    {
                        ids.Add(await Database.DeleteAllAsync(mapping).ConfigureAwait(false));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            return ids;
        }

        public Task BackupDatabaseAsync(string targetFolder, string databaseName) => Database?.BackupAsync(targetFolder, databaseName);
        #endregion

        #region Rekey
        public void RekeyDatabase(string newPassword)
        {
            // Bases on: https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/encryption?tabs=netcore-cli
            if (Database is not null)
            {
                SQLiteConnectionWithLock con = Database.GetConnection();
                SQLiteCommand command = con
                    .CreateCommand(
                        "SELECT quote($newPassword);",
                        new Dictionary<string, object>() { { "$newPassword", newPassword } }
                        );
                string quotedNewPassword = command.ExecuteScalar<string>();
                command = con
                    .CreateCommand(
                        $"PRAGMA rekey = {quotedNewPassword}"
                        );
                command.ExecuteNonQuery();
            }
        }

        public async Task RekeyDatabaseAsync(string newPassword)
        {
            // Bases on: https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/encryption?tabs=netcore-cli
            if (Database is not null)
            {
                string quotedNewPassword = await Database
                    .ExecuteScalarAsync<string>(
                        $"SELECT quote('{newPassword}');"
                        );
                await Database.ExecuteAsync($"PRAGMA rekey = {quotedNewPassword}");
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisconnectAsync().ConfigureAwait(false);
            }
        }
        #endregion
    }
}
