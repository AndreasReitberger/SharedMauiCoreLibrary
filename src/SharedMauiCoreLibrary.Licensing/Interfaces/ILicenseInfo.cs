namespace AndreasReitberger.Shared.Core.Licensing.Interfaces {
    public interface ILicenseInfo {

        #region Properties
        Guid Id { get; set; }
        string License { get; set; }
        string Domain { get; set; }
        string Owner { get; set; }
        string ProductCode { get; set; }
        DateTimeOffset LastCheck { get; set; }
        bool IsValid { get; set; }
        bool IsActive { get; set; }
        ILicenseOptions Options { get; set; }
        #endregion
    }
}
