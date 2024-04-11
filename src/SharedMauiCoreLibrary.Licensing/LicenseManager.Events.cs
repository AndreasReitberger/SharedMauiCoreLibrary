using AndreasReitberger.Shared.Core.Licensing.Events;
using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing
{
    public partial class LicenseManager : ObservableObject, ILicenseManager
    {
        #region Events
        public event EventHandler? Error;
        protected virtual void OnError()
        {
            Error?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnError(ErrorEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        protected virtual void OnError(UnhandledExceptionEventArgs e)
        {
            Error?.Invoke(this, e);
        }

        public event EventHandler? LicenseChanged;
        protected virtual void OnLicenseChanged(LicenseChangedEventArgs e)
        {
            LicenseChanged?.Invoke(this, e);
        }
        #endregion
    }
}
