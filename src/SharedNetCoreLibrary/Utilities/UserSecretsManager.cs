using CommunityToolkit.Mvvm.ComponentModel;
#if NEWTONSOFT
using Newtonsoft.Json.Linq;
#else
using System.Text.Json;
#endif
using System.Reflection;

namespace AndreasReitberger.Shared.Core.Utilities
{
    // Source: https://github.com/ncarandini/XFUserSecrets/blob/master/TPCWare.XFUserSecrets/Utils/UserSecretsManager.cs
    // Changed: Yes
    public partial class UserSecretsManager : ObservableObject
    {
        #region Instance
        static UserSecretsManager? _instance = null;
        static readonly object Lock = new();
        public static UserSecretsManager Settings
        {
            get
            {
                lock (Lock)
                {
                    _instance ??= new UserSecretsManager();
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
#if NEWTONSOFT
        JObject? _secrets;
#else
        JsonDocument? _secrets;
#endif
        #endregion

        #region Properties
        [ObservableProperty]
        public partial string AppNamespace { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string UserSecretsFileName { get; set; } = "secrets.json";

        [ObservableProperty]
        public partial Assembly? CurrentAssembly { get; set; }
        #endregion

        #region Ctor
        public UserSecretsManager() { }
        public UserSecretsManager(string appNamespace, string userSecretsFileName)
        {
            AppNamespace = appNamespace;
            UserSecretsFileName = userSecretsFileName;
        }
        #endregion

        #region Methods

        public void Initialize(Assembly? assembly = null)
        {
            try
            {
                CurrentAssembly ??= GetAssembly(typeof(UserSecretsManager));
                Stream? stream = CurrentAssembly?.GetManifestResourceStream($"{AppNamespace}.{UserSecretsFileName}");
                if (stream is not null)
                {
                    using StreamReader reader = new(stream);
                    string json = reader.ReadToEnd();
#if NEWTONSOFT
                    _secrets = JObject.Parse(json);
#else
                    _secrets = JsonDocument.Parse(json);
#endif
                }
            }
            catch (Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
            }
        }

        public T? ToObject<T>()
        {
            if (_secrets is null) return default;
#if NEWTONSOFT
            return _secrets.ToObject<T>();
#else
            return JsonSerializer.Deserialize<T>(_secrets.RootElement.GetRawText()); ;
#endif
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
#if NEWTONSOFT
                    JToken node = _secrets[path[0]];
                    for (int index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }
#else
                    JsonElement node = _secrets.RootElement;
                    foreach (var segment in path)
                    {
                        if (node.TryGetProperty(segment, out var child))
                        {
                            node = child;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
#endif
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

