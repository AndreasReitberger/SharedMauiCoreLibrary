namespace AndreasReitberger.Shared.Core.Licensing.Events
{
    public class LicenseErrorEventArgs : EventArgs
    {
        #region Properties
        public string ErrorMessage { get; set; } = string.Empty;
        public Exception? Exception { get; set; }
        #endregion

        #region Ctor
        public LicenseErrorEventArgs() { }
        public LicenseErrorEventArgs(Exception exc)
        {
            Exception = exc;
            ErrorMessage = exc.Message;
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, LicenseSourceGenerationContext.Default.LicenseErrorEventArgs);
        #endregion
    }
}
