namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IDispatchManager
    {
        #region Instance
        public static IDispatchManager? Instance { get; private set; }
        #endregion

        #region Properties
        public IDispatcher? Dispatcher { get; set; }
        #endregion

        #region Methods
        public void Dispatch(Action action, bool forceUiThread = false);
        public void Dispatch(Func<Task> action, bool forceUiThread = false);
        public Task DispatchAsync(Action action, bool forceUiThread = false);
        public Task DispatchAsync(Func<Task> action, bool forceUiThread = false);
        #endregion

        #region Events
        public event EventHandler? Error;
        #endregion
    }
}
