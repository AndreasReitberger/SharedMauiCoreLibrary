using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Database;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace AndreasReitberger.Shared.Firebase
{
    public partial class FirebaseHandler : ObservableObject
    {
        #region Variables
        readonly string appSecret = string.Empty;
        protected FirebaseAuthClient? authClient;
        #endregion

        #region Properties

        [ObservableProperty]
        public partial FirebaseAuthConfig? Config { get; set; }
        partial void OnConfigChanged(FirebaseAuthConfig? value)
        {
            if (value is not null)
                authClient = new(value);
        }

        [ObservableProperty]
        public partial UserCredential? CurrentUser { get; set; }
        partial void OnCurrentUserChanged(UserCredential? value)
        {
            OnCurrentUserChangedEvent(new()
            {
                User = value,
                LoggedIn = value?.OperationType == OperationType.SignIn && value?.User is not null,
            });
            if (value is null)
            {
                Token = string.Empty;
            }
        }
        partial void OnCurrentUserChanging(UserCredential? value)
        {
            Unsubscribe<object>();
        }

        [ObservableProperty]
        public partial string Token { get; set; } = string.Empty;
        partial void OnTokenChanged(string value)
        {
            //Unsubscribe<object>();
            // Force client to be refreshed with a new token
            client = null;
        }

        [ObservableProperty]
        public partial ConcurrentDictionary<string, Tuple<object, Type>> CloudSettings { get; set; } = [];
        partial void OnCloudSettingsChanged(ConcurrentDictionary<string, Tuple<object, Type>> value)
        {

        }

        #endregion

        #region Ctor

        public FirebaseHandler(string domain, string authenticationKey, string uri, string api, string secret, FirebaseAuthConfig config) : base()
        {
            Config = config;
            apiKey = api;
            authKey = authenticationKey;
            baseUri = uri;
            appSecret = secret;
            UseDefaultConfig();
            Instance = this;
        }
        #endregion

        #region Events
        static void AuthStateChanged(object? sender, UserEventArgs e)
        {
            // the callback is not guaranteed to be on UI thread
            //Dispatcher.Invoke(() =>
            //{
            Debug.WriteLine($"Firebase: Auth state changed!");
            if (e.User == null)
            {
                Debug.WriteLine($"Firebase: User was null");
                // no user is signed in (first run of the app, user signed out..), show login UI 
            }
            else
            {
                Debug.WriteLine($"Firebase: {e.User}");
            }
            //});
        }
        #endregion

        #region Config
        public void UseDefaultConfig()
        {
            Config = GetDefaultAuthConfig();
            authClient = new FirebaseAuthClient(Config);
            authClient.AuthStateChanged += AuthStateChanged;
        }

        public FirebaseAuthConfig GetDefaultAuthConfig()
        {
            return new()
            {
                ApiKey = apiKey,
                AuthDomain = authDomain,
                Providers =
                [
                    new EmailProvider(),
                    new GoogleProvider().AddScopes("email"),
                ],
                UserRepository = new FileUserRepository(appSecret),
            };
        }

        public FirebaseAuthConfig GetAuthConfig(params FirebaseAuthProvider[] providers)
        {
            return new()
            {
                ApiKey = apiKey,
                AuthDomain = authDomain,
                Providers = providers,
                UserRepository = new FileUserRepository(appSecret),
            };
        }
        #endregion

        #region Login / Logout Handling
        public FirebaseClient ConnectWithLogin(Func<Task<string>> login)
        {
            return new(baseUri, new FirebaseOptions
            {
                AuthTokenAsyncFactory = async () => await login().ConfigureAwait(false)
            });
        }

        public FirebaseClient ConnectWithToken(string token)
        {
            return new(baseUri, new FirebaseOptions
            {
                AuthTokenAsyncFactory = async () => await Task.FromResult(token).ConfigureAwait(false)
            });
        }

        public async Task<UserCredential?> LoginAnonymouslyAsync()
        {
            if (authClient is null) return null;

            CurrentUser = await authClient.SignInAnonymouslyAsync();
            Token = await GetUserTokenAsync(CurrentUser);
            return CurrentUser;
        }

        /// <summary>
        /// Login method for the <c>Subscribe</c> method.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Corresponding password</param>
        /// <returns>Token</returns>
        public async Task<string> LoginSubscribeAndGetTokenAsync(string username, string password)
        {
            if (authClient is null || CurrentUser is null) return string.Empty;
            // Avoid multiple logins if the session is actually still valid and the user hasn't changed
            if (CurrentUser?.User?.Info?.Email == username && CurrentUser?.User?.Credential.IsExpired() is false)
            {
                return CurrentUser?.User?.Credential?.IdToken ?? string.Empty;
            }
            CurrentUser = await authClient.SignInWithEmailAndPasswordAsync(username, password);
            // Refresh the token, so that the client is updated afterwards
            Token = await CurrentUser.User.GetIdTokenAsync() ?? string.Empty;
            return Token;
        }

        public async Task<string> LoginAndGetTokenAsync(string username, string password, Func<Tuple<string, Tuple<object, Type>>, Task> onDataChanged)
        {
            UserCredential? user = await LoginWithEmailAndPasswordAsync(username, password, onDataChanged);
            if (user is null) return string.Empty;
            return await user.User.GetIdTokenAsync() ?? string.Empty;
        }

        public async Task<UserCredential?> LoginWithEmailAndPasswordAsync(string username, string password, Func<Tuple<string, Tuple<object, Type>>, Task>? onDataChanged = null, bool subscribeSettingsChanges = true)
        {
            if (authClient is null) return null;

            CurrentUser = await authClient.SignInWithEmailAndPasswordAsync(username, password);
            Token = await GetUserTokenAsync(CurrentUser);
            CloudSettings = await GetAppSettingsConcurrentDictionaryAsync();
            if (subscribeSettingsChanges)
                SubscribeUserSettings(username: username, password: password, onDataChanged);

            return CurrentUser;
        }

        public async Task<UserCredential?> RegisterWithEmailAndPasswordAsync(string username, string password, string displayName, Func<Tuple<string, Tuple<object, Type>>, Task>? onDataChanged = null, bool subscribeSettingsChanges = true)
        {
            if (authClient is null) return null;

            CurrentUser = await authClient.CreateUserWithEmailAndPasswordAsync(username, password, displayName);
            Token = await GetUserTokenAsync(CurrentUser);
            CloudSettings = await GetAppSettingsConcurrentDictionaryAsync();
            if (subscribeSettingsChanges)
                SubscribeUserSettings(username: username, password: password, onDataChanged);

            return CurrentUser;
        }

        public static Task<string> GetUserTokenAsync(UserCredential user) => user.User.GetIdTokenAsync();

        public void Logout()
        {
            if (CurrentUser is null) return;
            authClient?.SignOut();
            CurrentUser = null;
        }
        #endregion
    }
}
