using AndreasReitberger.Shared.Core.Licensing.Interfaces;

namespace AndreasReitberger.Shared.Core.Licensing
{
    public partial class LicenseInfo
    {
        public class LicenseInfoBuilder
        {
            #region Instance
            readonly LicenseInfo _license = new();
            #endregion

            #region Methods

            public LicenseInfo Build()
            {
                return _license;
            }

            public LicenseInfoBuilder WithDomain(string domain)
            {
                _license.Domain = domain;
                return this;
            }

            public LicenseInfoBuilder WithProductIdentifier(string identifier)
            {
                _license.ProductCode = identifier;
                return this;
            }

            public LicenseInfoBuilder WithOptions(ILicenseOptions options)
            {
                _license.Options = options;
                return this;
            }
            
            public LicenseInfoBuilder WithLicense(string license)
            {
                _license.License = license;
                return this;
            }

            #endregion
        }
    }
}
