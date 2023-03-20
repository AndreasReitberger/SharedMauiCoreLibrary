namespace AndreasReitberger.Shared.Core.Licensing.Interfaces
{
    public interface ILicenseQueryResult
    {
        #region Properties
        public bool Success { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Message { get; set; }
        #endregion
    }
}
