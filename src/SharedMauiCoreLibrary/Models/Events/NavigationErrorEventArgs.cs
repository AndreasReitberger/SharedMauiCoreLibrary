namespace AndreasReitberger.Shared.Core.Events
{
    public partial class NavigationDoneEventArgs : EventArgs
    {
        #region Properties

        public Exception? Exception { get; set; }

        #endregion
    }
}
