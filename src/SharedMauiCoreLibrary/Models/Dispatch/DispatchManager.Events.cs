using AndreasReitberger.Shared.Core.Events;
using AndreasReitberger.Shared.Core.Interfaces;

namespace AndreasReitberger.Shared.Core.Dispatch
{
    public partial class DispatchManager : ObservableObject, IDispatchManager
    {
        #region Events
        public event EventHandler? Error;

        protected virtual void OnError()
        {
            Error?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnError(DispatchErrorEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        protected virtual void OnError(UnhandledExceptionEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        #endregion

    }
}
