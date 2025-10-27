using AndreasReitberger.Shared.Core.Events;

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

        public Task<bool> GoToAsync(IDispatcher dispatcher, string target, Dictionary<string, object>? parameters = null, bool? flyoutIsPresented = null, int delay = -1, bool animate = false);
        public Task<bool> GoToAsync(string target, Dictionary<string, object>? parameters = null, bool? flyoutIsPresented = null, int delay = -1, bool animate = false);
        public Task<bool> GoToRootAsync(IDispatcher dispatcher, string target, Dictionary<string, object>? parameters = null, bool? flyoutIsPresented = null, int delay = -1, bool animate = false);
        public Task<bool> GoToRootAsync(string target, Dictionary<string, object>? parameters = null, bool? flyoutIsPresented = null, int delay = -1, bool animate = false);
        public Task<bool> GoBackAsync(IDispatcher dispatcher, Dictionary<string, object>? parameters = null, bool? flyoutIsPresented = null, int delay = -1, bool animate = false, bool confirm = false, Func<Task<bool>>? confirmFunction = null);
        public Task<bool> GoBackAsync(Dictionary<string, object>? parameters = null, bool? flyoutIsPresented = null, int delay = -1, bool animate = false, bool confirm = false, Func<Task<bool>>? confirmFunction = null);
        bool IsCurrentPathRoot();
        #endregion

        #region Events
        public event EventHandler? Error;
        public event EventHandler<NavigationDoneEventArgs>? NavigationError;
        public event EventHandler<NavigationDoneEventArgs>? NavigationDone;
        #endregion
    }
}
