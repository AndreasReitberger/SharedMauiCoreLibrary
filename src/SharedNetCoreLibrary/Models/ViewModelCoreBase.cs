using AndreasReitberger.Shared.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AndreasReitberger.Shared.Core
{
    public partial class ViewModelCoreBase : ObservableObject, IViewModelCoreBase
    {
        #region Dependency Injection

        [ObservableProperty]
        public partial IServiceProvider? Provider { get; set; }
        #endregion

        #region Properties

        [ObservableProperty]
        public partial bool IsNavigatedTo { get; set; }

        [ObservableProperty]
        public partial bool DataLoaded { get; set; }

        [ObservableProperty]
        public partial bool IsLoading { get; set; } = false;

        [ObservableProperty]
        public partial bool IsLoadingData { get; set; } = false;

        [ObservableProperty]
        public partial int IsLoadingDataCounter { get; set; } = 0;
        partial void OnIsLoadingDataCounterChanged(int value)
        {
            // Avoid negative values
            if (value < 0) IsLoadingDataCounter = 0;
            IsLoadingData = value > 0;
        }

        [ObservableProperty]
        public partial bool IsBusy { get; set; } = false;

        [ObservableProperty]
        public partial int IsBusyCounter { get; set; } = 0;
        partial void OnIsBusyCounterChanged(int value)
        {
            // Avoid negative values
            if (value < 0) IsBusyCounter = 0;
            IsBusy = value > 0;
        }

        [ObservableProperty]
        public partial bool IsReady { get; set; } = false;

        [ObservableProperty]
        public partial bool IsStartUp { get; set; } = true;

        [ObservableProperty]
        public partial bool IsStartingUp { get; set; } = false;

        [ObservableProperty]
        public partial bool IsRefreshing { get; set; } = false;

        [ObservableProperty]
        public partial bool IsResuming { get; set; } = false;

        [ObservableProperty]
        public partial bool IsBeta { get; set; } = false;

        [ObservableProperty]
        public partial bool IsPortrait { get; set; } = true;

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

        #region Commands
        [RelayCommand]
        void NavigatedTo() => IsNavigatedTo = true;

        [RelayCommand]
        void NavigatedFrom() => IsNavigatedTo = false;
        #endregion

    }
}
