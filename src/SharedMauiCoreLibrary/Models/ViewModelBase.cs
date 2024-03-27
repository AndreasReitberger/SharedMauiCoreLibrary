﻿using AndreasReitberger.Shared.Core.Interfaces;
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

        [ObservableProperty]
        IFilePicker? filePicker;

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
        public ViewModelBase(IDispatcher dispatcher, IServiceProvider provider, IFileSaver? fileSaver) : base(provider: provider)
        {
            Dispatcher = dispatcher;
            Provider = provider;
            FileSaver = fileSaver;
        }
        public ViewModelBase(IDispatcher dispatcher, IServiceProvider provider, ILauncher? launcher) : base(provider: provider)
        {
            Dispatcher = dispatcher;
            Provider = provider;
            Launcher = launcher;
        }
        public ViewModelBase(IDispatcher dispatcher, IServiceProvider provider, IFilePicker? filePicker) : base(provider: provider)
        {
            Dispatcher = dispatcher;
            Provider = provider;
            FilePicker = filePicker;
        }
        public ViewModelBase(IDispatcher dispatcher, IServiceProvider provider, IFileSaver? fileSaver, ILauncher? launcher, IFilePicker? filePicker) : base(provider: provider)
        {
            Dispatcher = dispatcher;
            Provider = provider;
            FileSaver = fileSaver;
            Launcher = launcher;
            FilePicker = filePicker;
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
