using AndreasReitberger.Shared.Core.Update;

namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IUpdateManager
    {
        #region Properties
        public bool IsCheckingForUpdates { get; set; }
        #endregion

        #region Methods
        public Task<bool> CheckForUpdate();
        #endregion

        #region Events
        public event EventHandler<UpdateAvailableArgs>? UpdateAvailable;
        public event EventHandler? NoUpdateAvailable;
        public event EventHandler? ClientIncompatibleWithNewVersion;
        public event EventHandler? Error;
        #endregion
    }
}
