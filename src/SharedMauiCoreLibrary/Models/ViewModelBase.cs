using AndreasReitberger.Shared.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core
{
    public partial class ViewModelBase : ObservableObject, IViewModelBase
    {
        #region Dispatcher
        [ObservableProperty]
        IDispatcher dispatcher;
        #endregion

        #region Properties
        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        bool isBusy = false;

        [ObservableProperty]
        int isBusyCounter = 0;

        [ObservableProperty]
        bool isReady = false;

        [ObservableProperty]
        bool isStartUp = true;

        [ObservableProperty]
        bool _isStartingUp = false;

        [ObservableProperty]
        bool isRefreshing = false;

        [ObservableProperty]
        bool isResuming = false;

        [ObservableProperty]
        bool isBeta = false;

        [ObservableProperty]
        bool isPortrait = true;

        #endregion

        #region Ctor
        public ViewModelBase() { }
        public ViewModelBase(IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
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
