using AndreasReitberger.Shared.Core.Utilities;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

namespace AndreasReitberger.Shared.Firebase.Extensions
{
    public static class UserCredentialExtension
    {
        #region Methods

        public static Task SaveUserSettingsAsync(this UserCredential user, FirebaseClient client, Dictionary<string, Tuple<object, Type>> settings, string child = "settings")
            => client
                .Child("users")
                .Child(user.User.Uid)
                .Child(child)
                .PutAsync(settings);

        public static async Task<Dictionary<string, Tuple<object, Type>>> GetAppSettingsAsync(this UserCredential user, FirebaseClient client, string child = "settings")
        {
            Dictionary<string, Tuple<object, Type>> result = [];
            ChildQuery t = client
                .Child("users")
                .Child(user.User.Uid)
                .Child(child)
                ;
            string listJson = await t.OnceAsJsonAsync();
            result = JsonConvertHelper.ToObject<Dictionary<string, Tuple<object, Type>>>(listJson) ?? [];
            return result;
        }
        #endregion
    }
}
