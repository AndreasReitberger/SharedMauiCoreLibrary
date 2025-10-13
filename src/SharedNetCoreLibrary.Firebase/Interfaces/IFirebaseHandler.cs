using AndreasReitberger.Shared.Firebase.Events;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using System.Collections.Concurrent;

namespace AndreasReitberger.Shared.Firebase.Interfaces
{
    public interface IFirebaseHandler
    {
        #region Static
        public static FirebaseHandler? Instance;
        #endregion

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
        public void Subscribe<T>(Action<Tuple<string, T>> action, string username, string password, string child);
        public void Unsubscribe<T>();
        public void SubscribeUserSettings(string username, string password, Func<Tuple<string, Tuple<object, Type>>, Task>? onDataChanged);
        public void UseDefaultConfig();
        public FirebaseAuthConfig GetDefaultAuthConfig();
        public FirebaseAuthConfig GetAuthConfig(params FirebaseAuthProvider[] providers);
        public FirebaseClient ConnectWithLogin(Func<Task<string>> login);
        public FirebaseClient ConnectWithToken(string token);
        public Task<UserCredential?> LoginAnonymouslyAsync();
        public Task<string> LoginSubscribeAndGetTokenAsync(string username, string password);
        public Task<string> LoginAndGetTokenAsync(string username, string password, Func<Tuple<string, Tuple<object, Type>>, Task> onDataChanged);
        public Task<UserCredential?> LoginWithEmailAndPasswordAsync(string username, string password, Func<Tuple<string, Tuple<object, Type>>, Task>? onDataChanged, bool subscribeSettingsChanges);
        public Task<UserCredential?> RegisterWithEmailAndPasswordAsync(string username, string password, string displayName, Func<Tuple<string, Tuple<object, Type>>, Task>? onDataChanged, bool subscribeSettingsChanges);
        //public static Task<string> GetUserTokenAsync(UserCredential user);
        public void Logout();

        public Task<T?> GetDataAsync<T>(string child);
        public Task<bool> AddOrUpdateAppSettingsDictionaryAsync(Dictionary<string, Tuple<object, Type>> settings, string child);
        public Task AddOrUpdateAppSettingsAsync(Tuple<string, Tuple<object, Type>> settings, string child);
        public Task AddOrUpdateAppSettingsAsync(string settingsKey, Tuple<object, Type> settings, string child);
        public Task AddOrUpdateAppSettingsConcurrentDictionaryAsync(ConcurrentDictionary<string, Tuple<object, Type>> settings, string child);
        public Task AddOrUpdateUserDataAsync<T>(T data, string? child, bool appendData, string? key);
        public Task AddOrUpdateDefaultDataAsync<T>(T data, string child, bool appendData);
        public Task<Dictionary<string, Tuple<object, Type>>> GetAppSettingsDictionaryAsync(string child);
        public Task<ConcurrentDictionary<string, Tuple<object, Type>>> GetAppSettingsConcurrentDictionaryAsync(string child);
        public Task<T?> GetUserDataAsync<T>(string? child, string userPath, string? key);
        public Task<T?> GetDefaultDataAsync<T>(string child);
        #endregion
    }
}
