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

        public void Dispatch(IDispatcher? dispatcher, Action action, bool forceUiThread = false)
        {
            dispatcher ??= DispatcherProvider.Current.GetForCurrentThread();
            if (dispatcher is null)
            {
#if DEBUG
                // Show when a dispatching was done or not
                Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => The `Dispatcher` property was null!");
#endif
                return;
            }
#if DEBUG
            // Show when a dispatching was done or not
            Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => '{(dispatcher.IsDispatchRequired ? "dispatched" : "not dispatched")}'");
            if (dispatcher.IsDispatchRequired)
            {
                // Just for a breaking point
            }
#endif
            if (dispatcher.IsDispatchRequired || forceUiThread)
            {
                dispatcher.Dispatch(() =>
                {
                    try
                    {
                        action.Invoke();
                    }
                    catch (Exception exc)
                    {
                        OnError(new Events.DispatchErrorEventArgs(exc)
                        {
                            DispatchRequired = dispatcher.IsDispatchRequired
                        });
                    }
                });
            }
            else
            {
                action.Invoke();
            }
        }
        public void Dispatch(Action action, bool forceUiThread = false)
            => Dispatch(dispatcher: Dispatcher, action: action, forceUiThread: forceUiThread);
        
        public void Dispatch(IDispatcher? dispatcher, Func<Task> action, bool forceUiThread = false)
        {
            dispatcher ??= DispatcherProvider.Current.GetForCurrentThread(); 
            if (dispatcher is null)
            {
#if DEBUG
                // Show when a dispatching was done or not
                Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => The `Dispatcher` property was null!");
#endif
                return;
            }
#if DEBUG
            // Show when a dispatching was done or not
            Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => '{(dispatcher.IsDispatchRequired ? "dispatched" : "not dispatched")}'");
            if (dispatcher.IsDispatchRequired)
            {
                // Just for a breaking point
            }
#endif
            if (dispatcher.IsDispatchRequired || forceUiThread)
            {
                dispatcher.Dispatch(() =>
                {
                    try
                    {
                        action.Invoke();
                    }
                    catch (Exception exc)
                    {
                        OnError(new Events.DispatchErrorEventArgs(exc)
                        {
                            DispatchRequired = dispatcher.IsDispatchRequired
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
            => Dispatch(dispatcher: Dispatcher, action: action, forceUiThread: forceUiThread);

        public async Task DispatchAsync(IDispatcher? dispatcher, Action action, bool forceUiThread = false)
        {
            dispatcher ??= DispatcherProvider.Current.GetForCurrentThread();
            if (dispatcher is null)
            {
#if DEBUG
                // Show when a dispatching was done or not
                Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => The `Dispatcher` property was null!");
#endif
                return;
            }
#if DEBUG
            // Show when a dispatching was done or not
            Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => '{(dispatcher.IsDispatchRequired ? "dispatched" : "not dispatched")}'");
            if (dispatcher.IsDispatchRequired)
            {
                // Just for a breaking point
            }
#endif
            if (dispatcher.IsDispatchRequired || forceUiThread)
            {
                await dispatcher.DispatchAsync(() =>
                {
                    try
                    {
                        action.Invoke();
                    }
                    catch (Exception exc)
                    {
                        OnError(new Events.DispatchErrorEventArgs(exc)
                        {
                            DispatchRequired = dispatcher.IsDispatchRequired
                        });
                    }
                });
            }
            else
            {
                action.Invoke();
            }
        }
        public Task DispatchAsync(Action action, bool forceUiThread = false)
            => DispatchAsync(dispatcher: Dispatcher, action: action, forceUiThread: forceUiThread);

        public async Task DispatchAsync(IDispatcher? dispatcher, Func<Task> action, bool forceUiThread = false)
        {
            dispatcher ??= DispatcherProvider.Current.GetForCurrentThread();
            if (dispatcher is null)
            {
#if DEBUG
                // Show when a dispatching was done or not
                Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => The `Dispatcher` property was null!");
#endif
                return;
            }
#if DEBUG
            // Show when a dispatching was done or not
            Debug.WriteLine($"{nameof(DispatchManager)}: Dispatcher => '{(dispatcher.IsDispatchRequired ? "dispatched" : "not dispatched")}'");
            if (dispatcher.IsDispatchRequired)
            {
                // Just for a breaking point
            }
#endif
            if (dispatcher.IsDispatchRequired || forceUiThread)
            {
                await dispatcher.DispatchAsync(async () =>
                {
                    try
                    {
                        await action.Invoke();
                    }
                    catch (Exception exc)
                    {
                        OnError(new Events.DispatchErrorEventArgs(exc)
                        {
                            DispatchRequired = dispatcher.IsDispatchRequired
                        });
                    }
                });
            }
            else
            {
                await action.Invoke();
            }
        }
        public Task DispatchAsync(Func<Task> action, bool forceUiThread = false)
            => DispatchAsync(dispatcher: Dispatcher, action: action, forceUiThread: forceUiThread);
        #endregion

    }
}
