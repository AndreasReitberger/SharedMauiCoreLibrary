namespace AndreasReitberger.Shared.Core.Update
{
    public class UpdateAvailableArgs : EventArgs
    {
        #region Properties

        public Version? LatestVersion { get; set; }

        #endregion
    }
}
