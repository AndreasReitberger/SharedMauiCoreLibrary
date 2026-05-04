using AndreasReitberger.Shared.Core.Interfaces;
using SQLite;

namespace AndreasReitberger.Shared.Core.Database.Interfaces
{
    public interface ISqliteDatabaseService : IDisposable
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
        public string DatabasePath { get; set; }
        public string? Passphrase { get; set; }
        public SQLiteAsyncConnection? Database { get; set; }
        #endregion

        #region Methods
        public Task ConnectAsync();
        public Task DisconnectAsync();
        public Task<CreateTableResult?> CreateTableAsync<T>(CreateFlags flags) where T : new();
        public Task<List<T>?> GetAllWithChildrenAsync<T>(bool recursive) where T : new();
        public Task<T?> GetWithChildrenAsync<T>(object primaryKey, bool recursive) where T : new();
        public Task SetWithChildrenAsync<T>(T insert, bool replace = false, bool recursive = true) where T : new();
        public Task SetAllWithChildrenAsync<T>(IList<T> insert, bool replace = false, bool recursive = true) where T : new();
        public Task<int> DeleteWithChildrenAsync<T>(object primaryKey) where T : new();
        #endregion

        #region Rekey
        public void RekeyDatabase(string newPassword);
        public Task RekeyDatabaseAsync(string newPassword);
        #endregion
    }
}
