using Microsoft.Data.SqlClient;
using System.Data;

namespace AndreasReitberger.Shared.Core.Database.Interfaces
{
    public interface ISqlDatabaseService : IDisposable
    {
        #region Properties
        public string? ConnectionString { get; set; }
        public SqlCredential? Credentials { get; set; }
        #endregion

        #region Methods
        public void ExecuteCommand(string queryString);
        public long ExecuteScalarCommand(string queryString);
        public Task ExecuteCommandAsync(string queryString);
        public Task<long> ExecuteScalarCommandAsync(string queryString);
        public DataTable QueryCommand(string queryString);
        public Task<DataTable> QueryCommandAsync(string queryString);
        #endregion
    }
}
