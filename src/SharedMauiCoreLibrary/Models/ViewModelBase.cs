using AndreasReitberger.Shared.Core.Interfaces;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core
{
    public partial class ViewModelBase : ViewModelCoreBase, IViewModelBase
    {
        #region Dependency Injection
        [ObservableProperty]
        IDispatcher? dispatcher;

        [ObservableProperty]
        IFileSaver? fileSaver;

        [ObservableProperty]
        ILauncher? launcher;

        #endregion

        #region Ctor
        public ViewModelBase() : base() { }
        public ViewModelBase(IDispatcher dispatcher) : base()
        {
            Dispatcher = dispatcher;
        }
        public ViewModelBase(IDispatcher dispatcher, IServiceProvider provider) : base(provider: provider)
        {
            Dispatcher = dispatcher;
            Provider = provider;
        }
        public ViewModelBase(IDispatcher dispatcher, IServiceProvider provider, IFileSaver? fileSaver, ILauncher? launcher) : base(provider: provider)
        {
            Dispatcher = dispatcher;
            Provider = provider;
            FileSaver = fileSaver;
            Launcher = launcher;
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
