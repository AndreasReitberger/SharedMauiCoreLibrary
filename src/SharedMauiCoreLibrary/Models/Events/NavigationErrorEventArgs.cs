namespace AndreasReitberger.Shared.Core.Events
{
    public partial class NavigationErrorEventArgs : EventArgs
    {
        #region Properties

        public Exception? Exception { get; set; }

        #endregion
    }
}
