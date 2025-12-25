namespace AndreasReitberger.Shared.Core.Events
{
    public partial class ShellErrorEventArgs : EventArgs
    {
        #region Properties
        public Exception? Exception { get; set; }

        #endregion

        #region Ctor
        public ShellErrorEventArgs() { }
        public ShellErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }
        #endregion
    }
}
