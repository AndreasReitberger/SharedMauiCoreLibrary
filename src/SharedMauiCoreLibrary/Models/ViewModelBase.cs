using AndreasReitberger.Shared.Core.Interfaces;
using CommunityToolkit.Maui.Storage;

namespace AndreasReitberger.Shared.Core
{
    public partial class ViewModelBase : ViewModelCoreBase, IViewModelBase
    {
        #region Dependency Injection
        [ObservableProperty]
        public partial IDispatcher? Dispatcher { get; set; }

        [ObservableProperty]
        [Obsolete("Will be removed later. Use the `FileSaver.Default` from the `CommunityToolkit` instead.")]
        public partial IFileSaver? FileSaver { get; set; }

        [ObservableProperty]
        public partial ILauncher? Launcher { get; set; }

        [ObservableProperty]
        [Obsolete("Will be removed later. Use the `FilePicker.Default` from the `CommunityToolkit` instead.")]
        public partial IFilePicker? FilePicker { get;set; }

        #endregion

        #region Ctor
        public ViewModelBase() : base() { }
        public ViewModelBase(IDispatcher? dispatcher) : base()
        {
            Dispatcher = dispatcher;
        }
        public ViewModelBase(IDispatcher? dispatcher, IServiceProvider? provider) : base(provider: provider)
        {
            Dispatcher = dispatcher;
            Provider = provider;
        }
        [Obsolete("Will be removed later. Use the `FileSaver.Default` from the `CommunityToolkit` instead.")]
        public ViewModelBase(IDispatcher? dispatcher, IServiceProvider? provider, IFileSaver? fileSaver) : base(provider: provider)
        {
            Dispatcher = dispatcher;
            Provider = provider;
            FileSaver = fileSaver;
        }
        [Obsolete("Will be removed later. Use the `FileSaver.Default` from the `CommunityToolkit` instead.")]
        public ViewModelBase(IDispatcher? dispatcher, IServiceProvider? provider, IFileSaver? fileSaver, ILauncher? launcher) : base(provider: provider)
        {
            Dispatcher = dispatcher;
            Provider = provider;
            FileSaver = fileSaver;
            Launcher = launcher;
        }
        public ViewModelBase(IDispatcher? dispatcher, IServiceProvider? provider, ILauncher? launcher) : base(provider: provider)
        {
            Dispatcher = dispatcher;
            Provider = provider;
            Launcher = launcher;
        }
        [Obsolete("Will be removed later. Use the `FilePicker.Default` from the `CommunityToolkit` instead.")]
        public ViewModelBase(IDispatcher? dispatcher, IServiceProvider? provider, IFilePicker? filePicker) : base(provider: provider)
        {
            Dispatcher = dispatcher;
            Provider = provider;
            FilePicker = filePicker;
        }
        [Obsolete("Will be removed later. Use the `FilePicker.Default` and `FileSaver.Default` from the `CommunityToolkit` instead.")]
        public ViewModelBase(IDispatcher? dispatcher, IServiceProvider? provider, IFileSaver? fileSaver, ILauncher? launcher, IFilePicker? filePicker) : base(provider: provider)
        {
            Dispatcher = dispatcher;
            Provider = provider;
            FileSaver = fileSaver;
            Launcher = launcher;
            FilePicker = filePicker;
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
