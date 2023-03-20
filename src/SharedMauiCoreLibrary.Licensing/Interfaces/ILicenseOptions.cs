namespace AndreasReitberger.Shared.Core.Licensing.Interfaces
{
    public interface ILicenseOptions
    {
        #region Properties
        public bool VerifyLicenseFormat { get; set; }
        public string ProductName { get; set; }
        public string ProductIdentifier { get; set; }
        public string LicenseCheckPattern { get; set; }
        #endregion
    }
}
