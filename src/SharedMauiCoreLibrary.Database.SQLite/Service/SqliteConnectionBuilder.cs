namespace AndreasReitberger.Shared.Core.Database.Service
{
    public partial class SqliteDatabaseService
    {
        public class SqliteConnectionBuilder
        {
            #region Instance
            readonly SqliteDatabaseService _service = new(string.Empty);
            #endregion

            #region Methods

            public SqliteDatabaseService Build()
            {
                return _service;
            }

            public SqliteConnectionBuilder WithLocalPath(string localPath)
            {
                _service.DatabasePath = localPath;
                return this;
            }

            public SqliteConnectionBuilder WithPassphrase(string passphrase)
            {
                _service.Passphrase = passphrase;
                return this;
            }
            #endregion
        }
    }
}

