using AndreasReitberger.Shared.Core.Utilities;
using AndreasReitberger.Shared.Firebase.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json.Linq;

namespace AndreasReitberger.Shared.Firebase
{
    public partial class FirebaseHandler : IFirebaseHandler
    {
        #region Variables
        readonly string apiKey = string.Empty;
        readonly string authDomain = string.Empty;
        readonly string authKey = string.Empty;
        readonly string baseUri = string.Empty;

        protected FirebaseClient? client;
        #endregion

        #region Instance
        static FirebaseHandler? _instance;
        public static FirebaseHandler? Instance
        {
            get
            {
                //_instance ??= new();
                return _instance;
            }
            set
            {
                if (_instance == value) return;
                _instance = value;
            }
        }
        #endregion

        #region Constructor
        public FirebaseHandler(string authenticationKey) : base()
        {
            authKey = authenticationKey;

            UseDefaultConfig();
            Instance = this;
        }
        public FirebaseHandler(string authenticationKey, string uri) : base()
        {
            authKey = authenticationKey;
            baseUri = uri;

            UseDefaultConfig();
            Instance = this;
        }
        public FirebaseHandler(string domain, string authenticationKey, string uri, string api) : base()
        {
            apiKey = api;
            authKey = authenticationKey;
            baseUri = uri;      
            authDomain = domain;

            UseDefaultConfig();
            Instance = this;
        }
        #endregion

        #region Methods

        #region Subscription
        public void Subscribe<T>(Action<Tuple<string, T>> action, string username, string password, string child = "settings")
        {
            try
            {
                // Don't sync user data for anonymous logins
                if (CurrentUser?.User is null || CurrentUser.User.IsAnonymous == true) return;
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return;
                // Auth must be a function, otherwise the expired token cannot be refreshed.
                client = ConnectWithLogin(() => LoginSubscribeAndGetTokenAsync(username, password));
                IDisposable observable = client
                    .Child("users")
                    .Child(CurrentUser.User.Uid)
                    .Child(child)
                    .AsObservable<T>()
                    .Subscribe(data =>
                    {
                        try
                        {
                            action?.Invoke(new Tuple<string, T>(data.Key, data.Object));
                        }
                        catch (Exception ex)
                        {
                            OnErrorEvent(new(ex));
                            Unsubscribe<T>();
                        }
                    });
            }
            catch (Exception exc)
            {
                OnErrorEvent(new(exc));
            }
        }

        public void Unsubscribe<T>(string child = "settings")
        {
            try
            {
                if (client is null || CurrentUser is null) return;
                IDisposable observable = client
                    .Child("users")
                    .Child(CurrentUser.User.Uid)
                    .Child(child)
                    .AsObservable<T>()
                    .Subscribe();
            }
            catch (Exception exc)
            {
                OnErrorEvent(new(exc));
            }
        }

        public void SubscribeUserSettings(string username, string password, Func<Tuple<string, Tuple<object, Type>>, Task>? onDataChanged = null)
        {
            try
            {
                Subscribe<object>(async (p) =>
                {
                    if (p.Item2 is JObject json)
                    {
                        bool fire = false;
                        Tuple<object, Type>? data = JsonConvertHelper.ToObject<Tuple<object, Type>>(json.ToString());
                        if (data is not null)
                        {
                            CloudSettings ??= [];
                            if (CloudSettings.TryGetValue(p.Item1, out Tuple<object, Type>? value))
                            {
                                if (!value.Equals(data))
                                {
                                    CloudSettings?.TryUpdate(p.Item1, data, value);
                                    fire = true;
                                }
                            }
                            else
                            {
                                CloudSettings?.TryAdd(p.Item1, data);
                                fire = true;
                            }
                            if (fire)
                            {
                                // Pass back the changed data package to the caller
                                if (onDataChanged is not null) await onDataChanged.Invoke(new(p.Item1, data));
                                OnUserDataChangedEvent(new()
                                {
                                    User = CurrentUser,
                                    SettingsKey = p.Item1,
                                    ChangedSetting = data,
                                });
                            }
                        }
                    }
                }, username: username, password: password);
            }
            catch (Exception exc)
            {
                OnErrorEvent(new(exc));
            }
        }
        #endregion

        #endregion
    }
}
