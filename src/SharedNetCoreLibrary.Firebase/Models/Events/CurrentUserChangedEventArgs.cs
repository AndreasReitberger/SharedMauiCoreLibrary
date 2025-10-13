using Firebase.Auth;
using Newtonsoft.Json;

namespace AndreasReitberger.Shared.Firebase.Events
{
    public partial class CurrentUserChangedEventArgs : EventArgs
    {
        #region Properties
        public UserCredential? User { get; set; }

        public bool LoggedIn { get; set; } = false;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}
