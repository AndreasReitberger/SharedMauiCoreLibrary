namespace AndreasReitberger.Shared.Core.Events
{
    public partial class NavigationDoneEventArgs : EventArgs
    {
        #region Properties

        public Uri? NavigatedTo { get; set; }

        public Uri? NavigatedFrom { get; set; }

        public ShellNavigationSource? Source { get; set; } = ShellNavigationSource.Unknown;

        public ShellNavigatedEventArgs? Arguments { get; set; }

        #endregion
    }
}
