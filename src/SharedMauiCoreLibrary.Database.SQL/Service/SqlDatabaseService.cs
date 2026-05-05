using AndreasReitberger.Shared.Core.Database.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AndreasReitberger.Shared.Core.Database.Service
{
    public partial class SqlDatabaseService() : ObservableObject, ISqlDatabaseService
    {
        #region Properties

        [ObservableProperty]
        public partial string? ConnectionString { get; set; }

        [ObservableProperty]
        public partial SqlCredential? Credentials { get; set; }

        #endregion

        #region Ctor

        public SqlDatabaseService(string connectionString) : this()
        {
            ConnectionString = connectionString;
        }
        #endregion

        #region Methods

        public void ExecuteCommand(string queryString)
        {
            using SqlConnection connection = new(ConnectionString);
            SqlCommand cmd = new(queryString, connection);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
        }

        public long ExecuteScalarCommand(string queryString)
        {
            using SqlConnection connection = new(ConnectionString);
            SqlCommand cmd = new(queryString, connection);
            cmd.Connection.Open();
            object res = cmd.ExecuteScalar();
            long id;
            if (res.GetType() != typeof(DBNull))
                id = Convert.ToInt32(res);
            else
                id = -1;
            return id;
        }

        public async Task ExecuteCommandAsync(string queryString)
        {
            using SqlConnection connection = new(ConnectionString);
            SqlCommand cmd = new(queryString, connection);
            await cmd.Connection.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<long> ExecuteScalarCommandAsync(string queryString)
        {
            using SqlConnection connection = new(ConnectionString);
            SqlCommand cmd = new(queryString, connection);
            await cmd.Connection.OpenAsync();
            object? res = await cmd.ExecuteScalarAsync();
            long id;
            if (res?.GetType() != typeof(DBNull))
                id = Convert.ToInt32(res);
            else
                id = -1;
            return id;
        }

        public DataTable QueryCommand(string queryString)
        {
            DataTable result = new();
            using SqlConnection connection = new(ConnectionString);
            using SqlCommand cmd = new(queryString, connection);
            cmd.Connection.Open();
            SqlDataAdapter dataAdapter = new() { SelectCommand = cmd };
            dataAdapter.Fill(result);
            return result;
        }

        public async Task<DataTable> QueryCommandAsync(string queryString)
        {
            DataTable result = new();
            using SqlConnection connection = new(ConnectionString);         
            using SqlCommand cmd = new(queryString, connection);
            await cmd.Connection.OpenAsync();
            SqlDataAdapter dataAdapter = new() { SelectCommand = cmd };
            dataAdapter.Fill(result);        
            return result;
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

            }
        }
        #endregion
    }
}
