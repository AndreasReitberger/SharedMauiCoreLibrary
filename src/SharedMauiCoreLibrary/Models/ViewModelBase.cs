using AndreasReitberger.Shared.Core.Interfaces;
using CommunityToolkit.Maui.Storage;

namespace AndreasReitberger.Shared.Core
{
    public partial class ViewModelBase : ViewModelCoreBase, IViewModelBase
    {
        #region Dependency Injection
        [ObservableProperty]
        public partial IDispatcher? Dispatcher { get; set; }

        #endregion

        #region Ctor
        public ViewModelBase() : base() { 
        
            Dispatcher = DispatcherProvider.Current.GetForCurrentThread();
        }
        public ViewModelBase(IDispatcher? dispatcher) : base()
        {
            Dispatcher = dispatcher;
        }
        public ViewModelBase(IDispatcher? dispatcher, IServiceProvider? provider) : base(provider: provider)
        {
            Dispatcher = dispatcher;
            Provider = provider;
        }
        
        #endregion

        #region Methods
        public void SetBusy(bool isBusy, IDispatcher? dispatcher)
        {
            // Only dispatch if needed
            if (dispatcher is not null && dispatcher?.IsDispatchRequired is true)
            {
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
                if (isBusy)
                    IsBusyCounter++;
                else
                    IsBusyCounter--;
            }
        }

        public async Task SetBusyAsync(bool isBusy, IDispatcher? dispatcher)
        {
            // Only dispatch if needed
            if (dispatcher is not null && dispatcher?.IsDispatchRequired is true)
            {
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
                if (isBusy)
                    IsBusyCounter++;
                else
                    IsBusyCounter--;
            }
        }
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
