using AndreasReitberger.Shared.Firebase.SourceGeneration;
using Firebase.Auth;
using System.Text.Json;

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
        public override string ToString() => JsonSerializer.Serialize(this!, FirebaseSourceGenerationContext.Default.UserDataChangedEventArgs);

        #endregion
    }
}
