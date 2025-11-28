using AndreasReitberger.Shared.Core.Licensing.Enums;

namespace AndreasReitberger.Shared.Core.Licensing.Interfaces
{
    public interface ILicenseManager
    {
        #region Properties
        Uri? LicenseServer { get; set; }
        ILicenseInfo? CurrentLicense { get; set; }

        #endregion

        #region Methods
        //bool CheckLicense(ILicenseInfo license, LicenseServerTarget target);
        //bool ActivateLicense(ILicenseInfo license, LicenseServerTarget target);
        //bool DeactivateLicense(ILicenseInfo license, LicenseServerTarget target);

        Task<ILicenseQueryResult?> CheckLicenseAsync(ILicenseInfo license, LicenseServerTarget target, Func<string>? OnSuccess = null, Func<string>? OnError = null);
        Task<ILicenseQueryResult?> ActivateLicenseAsync(ILicenseInfo license, LicenseServerTarget target, Func<string>? OnSuccess = null, Func<string>? OnError = null);
        Task<ILicenseQueryResult?> DeactivateLicenseAsync(ILicenseInfo license, LicenseServerTarget target, Func<string>? OnSuccess = null, Func<string>? OnError = null);
        Task<IApplicationVersionResult?> GetLatestApplicationVersionAsync(ILicenseInfo license, LicenseServerTarget target, Func<string>? OnSuccess = null, Func<string>? OnError = null);
        Task<IApplicationVersionResult?> GetLatestApplicationVersionAsync(string domain, string productCode, LicenseServerTarget target, Func<string>? OnSuccess = null, Func<string>? OnError = null);
        #endregion

        #region Events
        event EventHandler? Error;
        event EventHandler? LicenseChanged;
        #endregion
    }
}
