namespace AndreasReitberger.Shared.Core.Licensing
{
    public partial class LicenseManager
    {
        public class LicenseManagerConnectionBuilder
        {
            #region Instance
            readonly LicenseManager _manager = new();
            #endregion

            #region Methods

            public LicenseManager Build()
            {
                _manager.Initialize();
                return _manager;
            }

            public LicenseManagerConnectionBuilder WithLicenseServer(Uri serverAddress, int? port = null)
            {
                _manager.LicenseServer = serverAddress;
                _manager.Port = port;
                return this;
            }

            public LicenseManagerConnectionBuilder WithLicenseServer(string serverAddress, int? port = null, bool https = false)
            {
                _manager.LicenseServer = new($"{(https ? "https" : "http")}://{serverAddress}");
                _manager.Port = port;
                return this;
            }
            public LicenseManagerConnectionBuilder WithAccessToken(string accessToken)
            {
                _manager.AccessToken = accessToken;
                return this;
            }
            #endregion
        }
    }
}

