namespace AndreasReitberger.Shared.Core.Licensing.Interfaces
{
    public interface IApplicationVersionResult
    {
        #region Properties

        public bool Success { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Version { get; set; }
        public string Message { get; set; }

        #endregion
    }
}
