using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace AndreasReitberger.Shared.Core.Utilities
{
    // Source: https://github.com/ncarandini/XFUserSecrets/blob/master/TPCWare.XFUserSecrets/Utils/UserSecretsManager.cs
    // Changed: Yes
    public partial class UserSecretsManager : ObservableObject
    {
        #region Instance
        static UserSecretsManager _instance = null;
        static readonly object Lock = new();
        public static UserSecretsManager Settings
        {
            get
            {
                lock (Lock)
                {
                    if (_instance == null)
                        _instance = new UserSecretsManager();
                }
                return _instance;
            }

            set
            {
                if (_instance == value) return;
                lock (Lock)
                {
                    _instance = value;
                }
            }

        }
        #endregion

        #region Variables
        JObject _secrets;
        #endregion

        #region Properties
        [ObservableProperty]
        string appNamespace;

        [ObservableProperty]
        string userSecretsFileName = "secrets.json";

        [ObservableProperty]
        Assembly currentAssembly;
        #endregion

        #region Ctor
        public UserSecretsManager() { }
        public UserSecretsManager(string appNamespace, string userSecretsFileName)
        {
            AppNamespace = appNamespace;
            UserSecretsFileName = userSecretsFileName;
        }
        /*
        private UserSecretsManager()
        {
            try
            {
                Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(UserSecretsManager)).Assembly;
                Stream stream = assembly.GetManifestResourceStream($"{Namespace}.{UserSecretsFileName}");
                using StreamReader reader = new(stream);
                string json = reader.ReadToEnd();
                _secrets = JObject.Parse(json);
            }
            catch (Exception ex)
            {
                EventManager.Instance.LogError(ex);
            }
        }
        */
        #endregion

        #region Methods

        public void Initialize(Assembly assembly = null)
        {
            try
            {
                //CurrentAssembly ??= IntrospectionExtensions.GetTypeInfo(typeof(UserSecretsManager)).Assembly;
                CurrentAssembly ??= GetAssembly(typeof(UserSecretsManager));
                Stream stream = CurrentAssembly.GetManifestResourceStream($"{AppNamespace}.{UserSecretsFileName}");
                using StreamReader reader = new(stream);
                string json = reader.ReadToEnd();
                _secrets = JObject.Parse(json);
            }
            catch (Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
            }
        }

        public T ToObject<T>()
        {
            return _secrets.ToObject<T>();
        }
        #endregion

        #region Static
#nullable enable
        public static Assembly? GetAssembly(Type type)
        {
            return IntrospectionExtensions.GetTypeInfo(type)?.Assembly;
        }
#nullable disable
        #endregion

        #region Extensions

        public string this[string name]
        {
            get
            {
                try
                {
                    var path = name.Split(':');

                    JToken node = _secrets[path[0]];
                    for (int index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }
                    return node.ToString();
                }
                catch (Exception ex)
                {
                    OnError(new ErrorEventArgs(ex));
                    return string.Empty;
                }
            }
        }
        #endregion

        #region Events

        public event EventHandler Error;
        protected virtual void OnError()
        {
            Error?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnError(ErrorEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        protected virtual void OnError(UnhandledExceptionEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        #endregion
    }
}

