using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using System.Collections.Concurrent;

namespace AndreasReitberger.Shared.Firebase.Interfaces
{
    public interface IFirebaseHandler
    {
        #region Properties
        public bool EnableEncryption { get; set; }
        public ConcurrentDictionary<Type, IDisposable> Subscriptions { get; set; }
        public FirebaseAuthConfig? Config { get; set; }
        public UserCredential? CurrentUser { get; set; }
        public string Token { get; set; }
        public ConcurrentDictionary<string, Tuple<object, Type>> CloudSettings { get; set; }

        #endregion

        #region Events
        public event EventHandler? ErrorEvent;

        public event EventHandler? CurrentUserChanged;

        public event EventHandler? UserDataChanged;
        #endregion

        #region Methods
        public void Subscribe<T>(Action<Tuple<string, T>> action, string username, string password, string child = "settings");
        public void Unsubscribe<T>();
        public void SubscribeUserSettings(string username, string password, Func<Tuple<string, Tuple<object, Type>>, Task>? onDataChanged = null);
        public void UseDefaultConfig();
        public FirebaseAuthConfig GetDefaultAuthConfig();
        public FirebaseAuthConfig GetAuthConfig(params FirebaseAuthProvider[] providers);
        public FirebaseClient ConnectWithLogin(Func<Task<string>> login);
        public FirebaseClient ConnectWithToken(string token);
        public Task<UserCredential?> LoginAnonymouslyAsync();
        public Task<string> LoginSubscribeAndGetTokenAsync(string username, string password);
        public Task<string> LoginAndGetTokenAsync(string username, string password, Func<Tuple<string, Tuple<object, Type>>, Task>? onDataChanged = null);
        public Task<UserCredential?> LoginWithEmailAndPasswordAsync(string username, string password, Func<Tuple<string, Tuple<object, Type>>, Task>? onDataChanged = null, bool subscribeSettingsChanges = true);
        public Task<UserCredential?> RegisterWithEmailAndPasswordAsync(string username, string password, string displayName, Func<Tuple<string, Tuple<object, Type>>, Task>? onDataChanged = null, bool subscribeSettingsChanges = true);
        //public static Task<string> GetUserTokenAsync(UserCredential user);
        public void Logout();

        public Task<T?> GetDataAsync<T>(string child);
        public Task<bool> AddOrUpdateAppSettingsDictionaryAsync(Dictionary<string, Tuple<object, Type>> settings, string child = "settings");
        public Task AddOrUpdateAppSettingsAsync(Tuple<string, Tuple<object, Type>> settings, string child = "settings");
        public Task AddOrUpdateAppSettingsAsync(string settingsKey, Tuple<object, Type> settings, string child = "settings");
        public Task AddOrUpdateAppSettingsConcurrentDictionaryAsync(ConcurrentDictionary<string, Tuple<object, Type>> settings, string child = "settings");
        public Task AddOrUpdateUserDataAsync<T>(T data, string? child = null, bool appendData = false, string? key = null);
        public Task AddOrUpdateDefaultDataAsync<T>(T data, string child, bool appendData = false);
        public Task<Dictionary<string, Tuple<object, Type>>> GetAppSettingsDictionaryAsync(string child = "settings");
        public Task<ConcurrentDictionary<string, Tuple<object, Type>>> GetAppSettingsConcurrentDictionaryAsync(string child = "settings");
        public Task<T?> GetUserDataAsync<T>(string? child = null, string userPath = "users", string? key = null);
        public Task<T?> GetDefaultDataAsync<T>(string child);
        #endregion
    }
}
