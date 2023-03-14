namespace AndreasReitberger.Shared.Core.Licensing.Interfaces {
    public interface ILicenseManager
    {
        #region Properties
        Uri LicenseServer { get; set; }

        #endregion

        #region Methods
        bool CheckLicense(ILicenseInfo license);
        bool ActivateLicense(ILicenseInfo license);
        bool DeactivateLicense(ILicenseInfo license);
        #endregion
    }
}
