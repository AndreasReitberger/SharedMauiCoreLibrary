using AndreasReitberger.Shared.Core.Events;

namespace AndreasReitberger.Shared.Core.NavigationManager
{
    public partial class ShellNavigator
    {
        #region Events
        public event EventHandler? Error;

        protected virtual void OnError()
        {
            Error?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnError(UnhandledExceptionEventArgs e)
        {
            Error?.Invoke(this, e);
        }

        public event EventHandler<NavigationDoneEventArgs>? NavigationError;
        protected virtual void OnNavigationError(NavigationDoneEventArgs e)
        {
            NavigationError?.Invoke(this, e);
        }

        public event EventHandler<NavigationDoneEventArgs>? NavigationDone;
        protected virtual void OnNavigationDone(NavigationDoneEventArgs e)
        {
            NavigationDone?.Invoke(this, e);
        }
        #endregion
    }
}
