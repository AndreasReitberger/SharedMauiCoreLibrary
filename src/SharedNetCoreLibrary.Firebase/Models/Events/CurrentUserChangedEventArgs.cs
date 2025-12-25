using AndreasReitberger.Shared.Firebase.SourceGeneration;
using Firebase.Auth;
using System.Text.Json;

namespace AndreasReitberger.Shared.Firebase.Events
{
    public partial class CurrentUserChangedEventArgs : EventArgs
    {
        #region Properties
        public UserCredential? User { get; set; }

        public bool LoggedIn { get; set; } = false;
        #endregion

        #region Overrides

        public override string ToString() => JsonSerializer.Serialize(this!, FirebaseSourceGenerationContext.Default.CurrentUserChangedEventArgs);

        #endregion
    }
}
