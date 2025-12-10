namespace AndreasReitberger.Shared.Core.Events
{
    public partial class DispatchErrorEventArgs : EventArgs
    {
        #region Properties
        public bool DispatchRequired { get; set; }
        public Exception? Exception { get; set; }

        #endregion

        #region Ctor
        public DispatchErrorEventArgs() { }
        public DispatchErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }
        #endregion
    }
}
