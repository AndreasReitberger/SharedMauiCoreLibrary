﻿using AndreasReitberger.Shared.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core
{
    public partial class ViewModelCoreBase : ObservableObject, IViewModelCoreBase
    {
        #region Dependency Injection

        [ObservableProperty]
        IServiceProvider? provider;
        #endregion

        #region Properties
        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        bool isLoadingData = false;

        [ObservableProperty]
        int isLoadingDataCounter = 0;
        partial void OnIsLoadingDataCounterChanged(int value)
        {
            // Avoid negative values
            if (value < 0) IsLoadingDataCounter = 0;
            IsLoadingData = value > 0;
        }

        [ObservableProperty]
        bool isBusy = false;

        [ObservableProperty]
        int isBusyCounter = 0;
        partial void OnIsBusyCounterChanged(int value)
        {
            // Avoid negative values
            if (value < 0) IsBusyCounter = 0;
            IsBusy = value > 0;
        }

        [ObservableProperty]
        bool isReady = false;

        [ObservableProperty]
        bool isStartUp = true;

        [ObservableProperty]
        bool isStartingUp = false;

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
        public ViewModelCoreBase() { }
        public ViewModelCoreBase(IServiceProvider? provider)
        {
            Provider = provider;
        }
        #endregion

        #region Methods
        public void SetBusy(bool isBusy)
        {
            if (isBusy)
                IsBusyCounter++;
            else
                IsBusyCounter--;
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
