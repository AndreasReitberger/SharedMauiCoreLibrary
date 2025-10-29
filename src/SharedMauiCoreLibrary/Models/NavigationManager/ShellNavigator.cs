using AndreasReitberger.Shared.Core.Events;
using AndreasReitberger.Shared.Core.Interfaces;

namespace AndreasReitberger.Shared.Core.NavigationManager
{
    /// <summary>
    /// A helper class to manage <c>Shell</c> navigations throughout the <c>App</c>
    /// </summary>
    public partial class ShellNavigator : ObservableObject, IShellNavigator
    {
        #region Instance
        static IShellNavigator? _instance = null;
#if NET9_0_OR_GREATER
        static readonly Lock Lock = new();
#else
        static readonly object Lock = new();
#endif
        public static IShellNavigator Instance
        {
            get
            {
                lock (Lock)
                {
                    _instance ??= new ShellNavigator();
                }
                return _instance;
            }
            set
            {
                if (_instance == value) return;
                lock (Lock)
                {
                    _instance = value;
                }
            }
        }
#endregion

        #region Properties

        public string CurrentRoute => GetCurrentRoute();

        [ObservableProperty]
        public partial IDispatcher? Dispatcher { get; set; }

        [ObservableProperty]
        public partial string PreviousRoute { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string RootPage { get; set; } = string.Empty;

        [ObservableProperty]
        public partial List<string> AvailableEntryPages { get; set; } = [];

        #endregion

        #region Constructor
        public ShellNavigator()
        {
            Dispatcher = DispatcherProvider.Current.GetForCurrentThread();
            Shell.Current.Navigated += (a, b) =>
            {
                string msg = $"Navigation: From '{b.Previous?.Location}' to '{b.Current?.Location}'. Source = '{b.Source}'";
                OnNavigationDone(new NavigationDoneEventArgs()
                {
                    NavigatedFrom = b.Previous?.Location,
                    NavigatedTo = b.Current?.Location,
                    Source = b.Source,
                });

            };
        }

        public ShellNavigator(string rootPage) : this()
        {
            RootPage = rootPage;
        }

        public ShellNavigator(IDispatcher dispatcher) : this()
        {
            Dispatcher = dispatcher;
        }

        public ShellNavigator(string rootPage, IDispatcher dispatcher) : this(rootPage)
        {
            Dispatcher = dispatcher;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Performs a navigation to the provided target, but handles it as root (navigation stack cleared).
        /// </summary>
        /// <param name="target">The name of the target route</param>
        /// <param name="flyoutIsPresented">Whether the flyout is kept open</param>
        /// <param name="delay">A delay in ms for the navigation</param>
        /// <param name="animate">Whether to animate the navigation</param>
        /// <param name="rootPrefix">The prefix for the root indication, default "///"</param>
        /// <returns><c>Task</c></returns>
        public Task<bool> GoToRootAsync(string target, Dictionary<string, object>? parameters = null, bool? flyoutIsPresented = null, int delay = -1, bool animate = true, string rootPrefix = "///")
            => GoToAsync(target: $"{rootPrefix}{target}", parameters, flyoutIsPresented, delay, animate);

        /// <summary>
        /// Performs a navigation to the provided target.
        /// </summary>
        /// <param name="target">The name of the target route</param>
        /// <param name="parameters">Query parameters passed to the navigated route</param>
        /// <param name="flyoutIsPresented">Whether the flyout is kept open</param>
        /// <param name="delay">A delay in ms for the navigation</param>
        /// <param name="animate">Whether to animate the navigation</param>
        /// <returns><c>Task</c></returns>
        public async Task<bool> GoToAsync(string target, Dictionary<string, object>? parameters = null, bool? flyoutIsPresented = null, int delay = -1, bool animate = true)
        {
            try
            {
                if (flyoutIsPresented is not null && Shell.Current.FlyoutBehavior == FlyoutBehavior.Flyout)
                {
                    Shell.Current.FlyoutIsPresented = flyoutIsPresented is true;
                }
                if (delay != -1)
                {
                    await Task.Delay(delay);
                }

                async Task<bool> navigationAction()
                {
                    try
                    {
                        PreviousRoute = GetCurrentRoute();
                        if (parameters == null)
                            await Shell.Current.GoToAsync(state: target, animate: animate);
                        else
                            await Shell.Current.GoToAsync(state: target, parameters: parameters, animate: animate);
                        return true;
                    }
                    catch (Exception exc)
                    {
                        OnNavigationError(new() { Exception = exc });
                        return false;
                    }
                }
                bool succeeded = false;
                if (Dispatcher is not null)
                {
                    succeeded = Dispatcher.IsDispatchRequired ? await Dispatcher.DispatchAsync(navigationAction) : await navigationAction();
                }
                else
                {
                    succeeded = await navigationAction();
                }
                return succeeded;
            }
            catch (Exception exc)
            {
                // Log error
                OnNavigationError(new() { Exception = exc });
                return false;
            }
        }
        
        /// <summary>
        /// Navigates back one route from the current navigation stack.
        /// </summary>
        /// <param name="parameters">Query parameters passed to the navigated route</param>
        /// <param name="flyoutIsPresented">Whether the flyout is kept open</param>
        /// <param name="delay">A delay in ms for the navigation</param>
        /// <param name="animate">Whether to animate the navigation</param>
        /// <param name="confirm">Whether to confirm the navigation</param>
        /// <param name="confirmFunction">Function which is executed to get the confirmation</param>
        /// <returns><c>Task</c></returns>
        public async Task<bool> GoBackAsync(Dictionary<string, object>? parameters = null, bool? flyoutIsPresented = null, int delay = -1, bool animate = true, bool confirm = false, Func<Task<bool>>? confirmFunction = null)
        {
            bool executeGoBack = true;
            if (confirm && confirmFunction is not null)
            {
                executeGoBack = await confirmFunction();
            }
            if (executeGoBack)
            {
                return await GoToAsync("..", parameters, flyoutIsPresented, delay, animate);
            }
            return false;
        }

        /// <summary>
        /// Returns true if the current path (route) is the root page
        /// </summary>
        /// <returns><c>true</c> if current path is root</returns>
        public bool IsCurrentPathRoot()
        {
            try
            {
                string[] parts = Shell.Current.CurrentState.Location.OriginalString.Split(["/"], StringSplitOptions.RemoveEmptyEntries);
                return parts.Length <= 1;
            }
            catch (Exception exc)
            {
                OnNavigationError(new() { Exception = exc });
                return false;
            }
        }

        /// <summary>
        /// Returns the current route as <c>string</c>
        /// </summary>
        /// <returns>Current route as <c>string</c></returns>
        string GetCurrentRoute()
        {
            try
            {
                ShellNavigationState state = Shell.Current.CurrentState;
                if (state == null) return string.Empty;

                string? lastPart = state.Location.ToString().Split('/').Where(x => !string.IsNullOrWhiteSpace(x)).LastOrDefault();
                return lastPart ?? string.Empty;
            }
            catch (Exception exc)
            {
                OnNavigationError(new() { Exception = exc });
                return string.Empty;
            }
        }

        #endregion

    }
}
