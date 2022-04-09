using AndreasReitberger.Shared.Core.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AndreasReitberger.Shared.Core
{
    public partial class ViewModelBase : IViewModelBase
    {
        #region Properties
        bool _isLoading = false;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        bool _isReady = false;
        public bool IsReady
        {
            get { return _isReady; }
            set { SetProperty(ref _isReady, value); }
        }
        
        bool _isStartUp = true;
        public bool IsStartUp
        {
            get { return _isStartUp; }
            set { SetProperty(ref _isStartUp, value); }
        }
        
        bool _isStartingUp = false;
        public bool IsStartingUp
        {
            get { return _isStartingUp; }
            set { SetProperty(ref _isStartingUp, value); }
        }
        
        bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }
        
        bool _isResuming = false;
        public bool IsResuming
        {
            get { return _isResuming; }
            set { SetProperty(ref _isResuming, value); }
        }
        
        bool _isBeta = false;
        public bool IsBeta
        {
            get { return _isBeta; }
            set { SetProperty(ref _isBeta, value); }
        }
        
        bool _isPortrait = true;
        public bool IsPortrait
        {
            get { return _isPortrait; }
            set { SetProperty(ref _isPortrait, value); }
        }
        #endregion

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Dispose
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
        #endregion

    }
}
