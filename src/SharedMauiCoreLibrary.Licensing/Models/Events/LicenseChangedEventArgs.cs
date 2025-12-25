namespace AndreasReitberger.Shared.Core.Licensing.Events
{
    public class LicenseChangedEventArgs : EventArgs
    {
        #region Properties
        public string Message { get; set; } = string.Empty;
        public DateTimeOffset CheckDate { get; set; }
        public string LicenseKey { get; set; } = string.Empty;
        public bool Valid { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, LicenseSourceGenerationContext.Default.LicenseChangedEventArgs);
        #endregion
    }
}
