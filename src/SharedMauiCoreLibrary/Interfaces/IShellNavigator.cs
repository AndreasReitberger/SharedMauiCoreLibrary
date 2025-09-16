namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IShellNavigator
    {
        #region Properties
        public string CurrentRoute { get; }
        public string PreviousRoute { get; }
        public string RootPage { get; }
        public List<string> AvailableEntryPages { get; }
        #endregion

        #region Methods

        public Task<bool> GoToAsync(IDispatcher dispatcher, string target, bool flyoutIsPresented = false, int delay = -1, bool animate = true);
        public Task<bool> GoToAsync(IDispatcher dispatcher, string target, Dictionary<string, object>? parameters = null, bool flyoutIsPresented = false, int delay = -1, bool animate = true);
        public Task GoBackAsync(IDispatcher dispatcher, bool flyoutIsPresented = false, int delay = -1, bool animate = true, bool confirm = false);
        public Task GoBackAsync(IDispatcher dispatcher, Dictionary<string, object>? parameters = null, bool flyoutIsPresented = false, int delay = -1, bool animate = true, bool confirm = false);
        bool IsCurrentPathRoot();
        //void RegisterRoutes();
        #endregion
    }
}
