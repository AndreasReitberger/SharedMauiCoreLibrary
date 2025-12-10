using AndreasReitberger.Shared.Core.Events;
using AndreasReitberger.Shared.Core.Interfaces;
using System.Diagnostics;

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
            Dispatcher ??= DispatcherProvider.Current.GetForCurrentThread();
            SubscribeNavigated();
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

        #region Dtor
        ~ShellNavigator()
        {
            UnsubscribeNavigated();
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
#if DEBUG
                    // Show when a dispatching was done or not
                    Debug.WriteLine($"{nameof(ShellNavigator)}: Dispatcher => '{(Dispatcher.IsDispatchRequired ? "dispatched" : "not dispatched")}'");
                    if (Dispatcher.IsDispatchRequired)
                    {
                        // Just for a breaking point
                    }
#endif
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
        /// Displays an alert dialog UIThread safe with the specified title, message, and buttons, and returns a value indicating
        /// which button was pressed.
        /// </summary>
        /// <remarks>If <paramref name="cancel"/> is <see langword="null"/>, the alert displays only the
        /// confirmation button and always returns <see langword="true"/> when dismissed. If <paramref name="cancel"/>
        /// is specified, the alert displays both buttons and returns <see langword="true"/> if the confirmation button
        /// is pressed, or <see langword="false"/> if the cancel button is pressed. The method returns <see
        /// langword="false"/> if an error occurs or if the alert cannot be displayed.</remarks>
        /// <param name="title">The title text to display at the top of the alert dialog.</param>
        /// <param name="message">The message content to display in the alert dialog.</param>
        /// <param name="ok">The text for the confirmation button. Selecting this button returns <see langword="true"/>.</param>
        /// <param name="cancel">The text for the cancel button. If specified, the alert will display both confirmation and cancel buttons;
        /// otherwise, only the confirmation button is shown. Selecting this button returns <see langword="false"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the
        /// confirmation button was pressed; otherwise, <see langword="false"/>.</returns>
        public async Task<bool> DisplayAlertAsync(string title, string message, string ok, string? cancel = null)
        {
            try
            {
                async Task<bool> action()
                {
                    try
                    {
                        bool result = false;
                        if (Shell.Current is null) return result;
                        if (cancel is null)
                        {
                            await Shell.Current
                                .DisplayAlertAsync(title, message, ok)
                                .ConfigureAwait(false);
                            result = true;
                        }
                        else
                            result = await Shell.Current
                                .DisplayAlertAsync(title, message, ok, cancel)
                                .ConfigureAwait(false);
                        return result;
                    }
                    catch (Exception exc)
                    {
                        OnError(new ShellErrorEventArgs(exc));
                        return false;
                    }
                }
                bool succeeded = false;
                if (Dispatcher is not null)
                {
#if DEBUG
                    // Show when a dispatching was done or not
                    Debug.WriteLine($"{nameof(ShellNavigator)}: Dispatcher => '{(Dispatcher.IsDispatchRequired ? "dispatched" : "not dispatched")}'");
                    if (Dispatcher.IsDispatchRequired)
                    {
                        // Just for a breaking point
                    }
#endif
                    succeeded = Dispatcher.IsDispatchRequired ? await Dispatcher.DispatchAsync(action) : await action();
                }
                else
                {
                    succeeded = await action();
                }
                return succeeded;
            }
            catch (Exception exc)
            {
                // Log error
                OnError(new ShellErrorEventArgs(exc));
                return false;
            }
        }

        /// <summary>
        /// Displays an action sheet UIThread safe to the user with a set of options and returns the selected option asynchronously.
        /// </summary>
        /// <remarks>If both cancel and destruction are null or empty, only the provided options are
        /// shown. The method may return null if the action sheet is dismissed or if an error occurs. This method must
        /// be called from a context where UI interaction is permitted.</remarks>
        /// <param name="title">The title to display at the top of the action sheet. Can be null or empty for no title.</param>
        /// <param name="cancel">The text for the cancel button. If null or empty, no cancel button is shown.</param>
        /// <param name="destruction">The text for the destruction button, typically used for a destructive action. If null or empty, no
        /// destruction button is shown.</param>
        /// <param name="buttons">An array of button labels representing the available options for the user to select. Cannot be null; may be
        /// empty for no options.</param>
        /// <returns>A task that represents the asynchronous operation. The result is the label of the button selected by the
        /// user, or null if the action sheet was dismissed without selection.</returns>
        public async Task<string?> DisplayActionSheetAsync(string title, string cancel, string? destruction = null, params string[] buttons)
        {
            try
            {
                string? prompt = null;
                async Task<string?> action()
                {
                    try
                    {
                        if (Shell.Current is null) return null;
                        return await Shell.Current
                            .DisplayActionSheetAsync(title, cancel, destruction, buttons)
                            .ConfigureAwait(false);
                    }
                    catch (Exception exc)
                    {
                        OnError(new ShellErrorEventArgs(exc));
                        return null;
                    }
                }
                if (Dispatcher is not null)
                {
#if DEBUG
                    // Show when a dispatching was done or not
                    Debug.WriteLine($"{nameof(ShellNavigator)}: Dispatcher => '{(Dispatcher.IsDispatchRequired ? "dispatched" : "not dispatched")}'");
                    if (Dispatcher.IsDispatchRequired)
                    {
                        // Just for a breaking point
                    }
#endif
                    prompt = Dispatcher.IsDispatchRequired ? await Dispatcher.DispatchAsync(action) : await action();
                }
                else
                {
                    prompt = await action();
                }
                return prompt;
            }
            catch (Exception exc)
            {
                // Log error
                OnError(new ShellErrorEventArgs(exc));
                return null;
            }
        }

        /// <summary>
        /// Displays an asynchronous prompt dialog UIThread safe to the user and returns the entered text, or null if the prompt is
        /// canceled.
        /// </summary>
        /// <remarks>If an error occurs while displaying the prompt, the method returns null and triggers
        /// an error event. The prompt dialog is dispatched to the UI thread if required. The method supports
        /// customization of the prompt's appearance and behavior through its parameters.</remarks>
        /// <param name="title">The title text displayed at the top of the prompt dialog.</param>
        /// <param name="message">The message or question presented to the user within the prompt dialog.</param>
        /// <param name="ok">The text for the confirmation button that submits the entered value.</param>
        /// <param name="cancel">The text for the cancel button that dismisses the prompt without submitting a value. Defaults to "Cancel".</param>
        /// <param name="placeholder">The placeholder text shown in the input field when it is empty. If null, no placeholder is displayed.</param>
        /// <param name="maxLength">The maximum number of characters allowed in the input field. Specify -1 for no limit.</param>
        /// <param name="keyboard">The keyboard type to use for the input field, such as numeric or email. If null, the default keyboard is
        /// used.</param>
        /// <param name="initialValue">The initial value displayed in the input field when the prompt appears. If null, the field is empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the text entered by the user, or null
        /// if the prompt is canceled or an error occurs.</returns>
        public async Task<string?> DisplayPromptAsync(string title, string message, string ok, string cancel = "Cancel", string? placeholder = null, int maxLength = -1, Keyboard? keyboard = null, string? initialValue = null)
        {
            try
            {
                string? prompt = null;
                async Task<string?> action()
                {
                    try
                    {
                        if (Shell.Current is null) return null;
                        return await Shell.Current
                            .DisplayPromptAsync(title, message, ok, cancel, placeholder, maxLength, keyboard, initialValue)
                            .ConfigureAwait(false);
                    }
                    catch (Exception exc)
                    {
                        OnError(new ShellErrorEventArgs(exc));
                        return null;
                    }
                }
                if (Dispatcher is not null)
                {
#if DEBUG
                    // Show when a dispatching was done or not
                    Debug.WriteLine($"{nameof(ShellNavigator)}: Dispatcher => '{(Dispatcher.IsDispatchRequired ? "dispatched" : "not dispatched")}'");
                    if (Dispatcher.IsDispatchRequired)
                    {
                        // Just for a breaking point
                    }
#endif
                    prompt = Dispatcher.IsDispatchRequired ? await Dispatcher.DispatchAsync(action) : await action();
                }
                else
                {
                    prompt = await action();
                }
                return prompt;
            }
            catch (Exception exc)
            {
                // Log error
                OnError(new ShellErrorEventArgs(exc));
                return null;
            }
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

        public void SubscribeNavigated()
        {
            if (Shell.Current is not null)
            {
                UnsubscribeNavigated();
                Shell.Current.Navigated += OnNavigated;
            }
        }
        public void UnsubscribeNavigated()
        {
            if (Shell.Current is not null)
            {
                Shell.Current.Navigated -= OnNavigated;
            }
        }

        private void OnNavigated(object? sender, ShellNavigatedEventArgs e)
        {
#if DEBUG
            string msg = $"Navigation: From '{e.Previous?.Location}' to '{e.Current?.Location}'. Source = '{e.Source}'";
            Debug.WriteLine(msg);
#endif
            OnNavigationDone(new NavigationDoneEventArgs()
            {
                Arguments = e,
                NavigatedFrom = e.Previous?.Location,
                NavigatedTo = e.Current?.Location,
                Source = e.Source,
            });
        }
        #endregion

    }
}
