using AndreasReitberger.Shared.Core.Interfaces;
#if DEBUG
using System.Diagnostics;
#endif

namespace AndreasReitberger.Shared.Core
{
    public partial class ViewModelBase : ViewModelCoreBase, IViewModelBase
    {
        #region Dependency Injection
        [ObservableProperty]
        public partial IDispatcher? Dispatcher { get; set; }

        #endregion

        #region Ctor
        public ViewModelBase() : base()
        {
            Dispatcher = DispatcherProvider.Current.GetForCurrentThread();
#if DEBUG
            Debug.WriteLine($"[ViewModelBase] Default Ctor called: {Dispatcher}");
#endif
        }
        public ViewModelBase(IDispatcher? dispatcher) : base()
        {
            Dispatcher = dispatcher;
#if DEBUG
            Debug.WriteLine($"[ViewModelBase] Ctor with dispatcher parameter called: {Dispatcher}");
#endif
        }
        public ViewModelBase(IDispatcher? dispatcher, IServiceProvider? provider) : base(provider: provider)
        {
            Dispatcher = dispatcher;
            Provider = provider;
#if DEBUG
            Debug.WriteLine($"[ViewModelBase] Ctor with dispatcher and provider parameter called: {Dispatcher}, {Provider}");
#endif
        }

        #endregion

        #region Methods
        public void SetBusy(bool isBusy, IDispatcher? dispatcher)
        {
            dispatcher ??= Dispatcher;
            // Only dispatch if needed
            if (dispatcher is not null && dispatcher.IsDispatchRequired is true)
            {
#if DEBUG
                Debug.WriteLine($"[ViewModelBase] SetBusy dispatching on ThreadId: {Environment.CurrentManagedThreadId}");
#endif
                dispatcher.Dispatch(() =>
                {
                    if (isBusy)
                        IsBusyCounter++;
                    else
                        IsBusyCounter--;
                });
            }
            // Update on the MainThread
            else
            {
#if DEBUG
                Debug.WriteLine($"[ViewModelBase] SetBusy executing on MainThreadId: {Environment.CurrentManagedThreadId}");
#endif
                if (isBusy)
                    IsBusyCounter++;
                else
                    IsBusyCounter--;
            }
        }
        public new void SetBusy(bool isBusy)
            => SetBusy(isBusy, Dispatcher);

        public async Task SetBusyAsync(bool isBusy, IDispatcher? dispatcher)
        {
            dispatcher ??= Dispatcher;
            // Only dispatch if needed
            if (dispatcher is not null && dispatcher.IsDispatchRequired)
            {
#if DEBUG
                Debug.WriteLine($"[ViewModelBase] SetBusyAsync dispatching on ThreadId: {Environment.CurrentManagedThreadId}");
#endif
                await dispatcher.DispatchAsync(() =>
                {
                    if (isBusy)
                        IsBusyCounter++;
                    else
                        IsBusyCounter--;
                });
            }
            // Update on the MainThread
            else
            {
#if DEBUG
                Debug.WriteLine($"[ViewModelBase] SetBusyAsync executing on MainThreadId: {Environment.CurrentManagedThreadId}");
#endif
                if (isBusy)
                    IsBusyCounter++;
                else
                    IsBusyCounter--;
            }
        }
        public Task SetBusyAsync(bool isBusy)
            => SetBusyAsync(isBusy, Dispatcher);
        #endregion

        #region Dispose
        /*
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            // Ordinarily, we release unmanaged resources here;
            // but all are wrapped by safe handles.

            // Release disposable objects.
            if (disposing)
            {

            }
        }
        */
        #endregion

    }
}
