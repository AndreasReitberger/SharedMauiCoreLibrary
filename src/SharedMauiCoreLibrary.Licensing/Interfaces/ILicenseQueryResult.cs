namespace AndreasReitberger.Shared.Core.Licensing.Interfaces
{
    public interface ILicenseQueryResult
    {
        #region Properties
        /// <summary>
        /// True if the request / check succeeded
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// True if the license is valid
        /// </summary>
        public bool Valid { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Message { get; set; }
        #endregion
    }
}
