using AndreasReitberger.Shared.Firebase.Events;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Firebase
{
    public partial class FirebaseHandler : ObservableObject
    {
        #region Events
        public event EventHandler? ErrorEvent;
        protected virtual void OnErrorEvent(ErrorEventArgs e)
        {
            ErrorEvent?.Invoke(this, e);
        }

        public event EventHandler? CurrentUserChanged;
        protected virtual void OnCurrentUserChangedEvent(CurrentUserChangedEventArgs e)
        {
            CurrentUserChanged?.Invoke(this, e);
        }

        public event EventHandler? UserDataChanged;
        protected virtual void OnUserDataChangedEvent(UserDataChangedEventArgs e)
        {
            UserDataChanged?.Invoke(this, e);
        }
        #endregion
    }
}
