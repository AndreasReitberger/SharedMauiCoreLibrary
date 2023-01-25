namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IShellNavigator
    {
        #region Properties
        string CurrentRoute { get; }
        string PreviousRoute { get; }
        string RootPage { get; }
        List<string> AvailableEntryPages { get; }
        #endregion

        #region Methods

        Task<bool> GoToAsync(string target, bool flyoutIsPresented = false, int delay = -1, bool animate = true);
        Task<bool> GoToAsync(string target, Dictionary<string, object> parameters = null, bool flyoutIsPresented = false, int delay = -1, bool animate = true);
        Task GoBackAsync(bool flyoutIsPresented = false, int delay = -1, bool animate = true, bool confirm = false);
        Task GoBackAsync(Dictionary<string, object> parameters = null, bool flyoutIsPresented = false, int delay = -1, bool animate = true, bool confirm = false);
        bool IsCurrentPathRoot();
        void RegisterRoutes();
        #endregion
    }
}
