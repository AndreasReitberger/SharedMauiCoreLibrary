namespace AndreasReitberger.Shared.Core.Database.Service
{
    public partial class SqlDatabaseService
    {
        public class SqlConnectionBuilder
        {
            #region Instance
            readonly SqlDatabaseService _service = new();
            #endregion

            #region Methods

            public SqlDatabaseService Build()
            {
                return _service;
            }

            public SqlConnectionBuilder WithConnectionString(string connectionString)
            {
                _service.ConnectionString = connectionString;
                return this;
            }
            #endregion
        }
    }
}

