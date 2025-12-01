using AndreasReitberger.Shared.Core.Events;

namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IShellNavigator
    {
        #region Properties
        public string CurrentRoute { get; }
        public string PreviousRoute { get; set; }
        public IDispatcher? Dispatcher { get; set; }
        public string RootPage { get; set; }
        public List<string> AvailableEntryPages { get; set; }
        #endregion

        #region Methods

        public Task<bool> GoToAsync(string target, Dictionary<string, object>? parameters = null, bool? flyoutIsPresented = null, int delay = -1, bool animate = false);
        public Task<bool> GoToRootAsync(string target, Dictionary<string, object>? parameters = null, bool? flyoutIsPresented = null, int delay = -1, bool animate = false, string rootPrefix = "///");
        public Task<bool> GoBackAsync(Dictionary<string, object>? parameters = null, bool? flyoutIsPresented = null, int delay = -1, bool animate = false, bool confirm = false, Func<Task<bool>>? confirmFunction = null);
        bool IsCurrentPathRoot();
        public void SubscribeNavigated();
        public void UnsubscribeNavigated();
        #endregion

        #region Events
        public event EventHandler? Error;
        public event EventHandler<NavigationDoneEventArgs>? NavigationError;
        public event EventHandler<NavigationDoneEventArgs>? NavigationDone;
        #endregion
    }
}
