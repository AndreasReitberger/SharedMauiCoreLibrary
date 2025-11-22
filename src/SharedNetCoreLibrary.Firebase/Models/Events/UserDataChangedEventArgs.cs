using Firebase.Auth;
using Newtonsoft.Json;

namespace AndreasReitberger.Shared.Firebase.Events
{
    public partial class UserDataChangedEventArgs : EventArgs
    {
        #region Properties
        public UserCredential? User { get; set; }
        public string SettingsKey { get; set; } = string.Empty;
        public Tuple<object, Type>? ChangedSetting { get; set; }

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}
