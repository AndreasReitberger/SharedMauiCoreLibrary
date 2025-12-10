using AndreasReitberger.Shared.Core.Interfaces;
using System.Diagnostics;

namespace AndreasReitberger.Shared.Core.Dispatch
{
    public partial class DispatchManager : ObservableObject, IDispatchManager
    {

        #region Instance
        static IDispatchManager? _instance = null;
#if NET9_0_OR_GREATER
        static readonly Lock Lock = new();
#else
        static readonly object Lock = new();
#endif
        public static IDispatchManager Instance
        {
            get
            {
                lock (Lock)
                {
                    _instance ??= new DispatchManager();
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
        [ObservableProperty]
        public partial IDispatcher? Dispatcher { get; set; }
        #endregion

        #region Ctor
        public DispatchManager()
        {
            Dispatcher ??= DispatcherProvider.Current.GetForCurrentThread();
        }
        public DispatchManager(IDispatcher? dispatcher) : this()
        {
            Dispatcher = dispatcher;
        }
        #endregion

        #region Methods

        public void Dispatch(Action action, bool forceUiThread = false)
        {
            Dispatcher ??= DispatcherProvider.Current.GetForCurrentThread();
            if (Dispatcher is null)
            {
#if DEBUG
                // Show when a dispatching was done or not
                Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => The `Dispatcher` property was null!");
#endif
                return;
            }
#if DEBUG
            // Show when a dispatching was done or not
            Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => '{(Dispatcher.IsDispatchRequired ? "dispatched" : "not dispatched")}'");
            if (Dispatcher.IsDispatchRequired)
            {
                // Just for a breaking point
            }
#endif
            if (Dispatcher.IsDispatchRequired || forceUiThread)
            {
                Dispatcher.Dispatch(() =>
                {
                    try
                    {
                        action.Invoke();
                    }
                    catch (Exception exc)
                    {
                        OnError(new Events.DispatchErrorEventArgs(exc)
                        {
                            DispatchRequired = Dispatcher.IsDispatchRequired
                        });
                    }
                });
            }
            else
            {
                action.Invoke();
            }
        }

        public void Dispatch(Func<Task> action, bool forceUiThread = false)
        {
            Dispatcher ??= DispatcherProvider.Current.GetForCurrentThread(); 
            if (Dispatcher is null)
            {
#if DEBUG
                // Show when a dispatching was done or not
                Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => The `Dispatcher` property was null!");
#endif
                return;
            }
#if DEBUG
            // Show when a dispatching was done or not
            Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => '{(Dispatcher.IsDispatchRequired ? "dispatched" : "not dispatched")}'");
            if (Dispatcher.IsDispatchRequired)
            {
                // Just for a breaking point
            }
#endif
            if (Dispatcher.IsDispatchRequired || forceUiThread)
            {
                Dispatcher.Dispatch(() =>
                {
                    try
                    {
                        action.Invoke();
                    }
                    catch (Exception exc)
                    {
                        OnError(new Events.DispatchErrorEventArgs(exc)
                        {
                            DispatchRequired = Dispatcher.IsDispatchRequired
                        });
                    }
                });
            }
            else
            {
                action.Invoke();
            }
        }

        /// <returns><c>Task</c></returns>
        public async Task DispatchAsync(Action action, bool forceUiThread = false)
        {
            Dispatcher ??= DispatcherProvider.Current.GetForCurrentThread();
            if (Dispatcher is null)
            {
#if DEBUG
                // Show when a dispatching was done or not
                Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => The `Dispatcher` property was null!");
#endif
                return;
            }
#if DEBUG
            // Show when a dispatching was done or not
            Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => '{(Dispatcher.IsDispatchRequired ? "dispatched" : "not dispatched")}'");
            if (Dispatcher.IsDispatchRequired)
            {
                // Just for a breaking point
            }
#endif
            if (Dispatcher.IsDispatchRequired || forceUiThread)
            {
                await Dispatcher.DispatchAsync(() =>
                {
                    try
                    {
                        action.Invoke();
                    }
                    catch (Exception exc)
                    {
                        OnError(new Events.DispatchErrorEventArgs(exc)
                        {
                            DispatchRequired = Dispatcher.IsDispatchRequired
                        });
                    }
                });
            }
            else
            {
                action.Invoke();
            }
        }

        public async Task DispatchAsync(Func<Task> action, bool forceUiThread = false)
        {
            Dispatcher ??= DispatcherProvider.Current.GetForCurrentThread();
            if (Dispatcher is null)
            {
#if DEBUG
                // Show when a dispatching was done or not
                Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => The `Dispatcher` property was null!");
#endif
                return;
            }
#if DEBUG
            // Show when a dispatching was done or not
            Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => '{(Dispatcher.IsDispatchRequired ? "dispatched" : "not dispatched")}'");
            if (Dispatcher.IsDispatchRequired)
            {
                // Just for a breaking point
            }
#endif
            if (Dispatcher.IsDispatchRequired || forceUiThread)
            {
                await Dispatcher.DispatchAsync(async () =>
                {
                    try
                    {
                        await action.Invoke();
                    }
                    catch (Exception exc)
                    {
                        OnError(new Events.DispatchErrorEventArgs(exc)
                        {
                            DispatchRequired = Dispatcher.IsDispatchRequired
                        });
                    }
                });
            }
            else
            {
                await action.Invoke();
            }
        }
        #endregion

    }
}
