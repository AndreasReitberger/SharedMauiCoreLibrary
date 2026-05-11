using AndreasReitberger.Shared.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
#if NEWTONSOFT
using Newtonsoft.Json.Linq;
#endif
using System.Reflection;

namespace AndreasReitberger.Shared.Core.Utilities
{
    // Source: https://github.com/ncarandini/XFUserSecrets/blob/master/TPCWare.XFUserSecrets/Utils/UserSecretsManager.cs
    // Changed: Yes
    public partial class UserSecretsManager : ObservableObject, IUserSecretsManager
    {
        #region Instance
        static IUserSecretsManager? _instance = null;
#if NET9_0_OR_GREATER
        static readonly Lock Lock = new();
#else
        static readonly object Lock = new();
#endif
        public static IUserSecretsManager Settings
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

        JsonDocument? _secrets;

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
        public UserSecretsManager(string appNamespace, string userSecretsFileName, Assembly assembly) : this(appNamespace, userSecretsFileName)
        {
            CurrentAssembly = assembly;
        }
        #endregion

        #region Methods

        public void Initialize(Assembly? assembly = null)
        {
            try
            {
                // Override in case a custom assembly is provided
                if (assembly is not null)
                    CurrentAssembly = assembly;
#if NET8_0_OR_GREATER
                ArgumentNullException.ThrowIfNull(CurrentAssembly, nameof(CurrentAssembly));
#else
                if (CurrentAssembly is null)
                    throw new ArgumentNullException($"The `{nameof(CurrentAssembly)}` cannot be null!");
#endif
                //CurrentAssembly ??= GetAssembly(typeof(UserSecretsManager));
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
        public T? ToObject<T>(JsonSerializerContext? context = null)
        {
            if (_secrets is null) return default;
            context ??= CoreSourceGenerationContext.Default;
            return (T?)JsonSerializer.Deserialize(_secrets.RootElement.GetRawText(), typeof(T), context);
        }
        public T? ReadSection<T>(string sectionName, JsonSerializerContext? context = null)
        {
            // Needs the Directory.Build.targets in order to work (copies the secret.json as EmbeddedResource to the app)
            string settings = Settings[sectionName].ToString();
            context ??= CoreSourceGenerationContext.Default;
            return (T?)JsonSerializer.Deserialize(settings, typeof(T), context);
        }
        public T? ReadSectionFromConfigurationRoot<T>(Type type, string sectionName, JsonSerializerContext? context = null)
        {
            // It seems that this way makes problems if the app is published on Windows in Release mode
            // Needs the Directory.Build.targets in order to work (copies the secret.json as EmbeddedResource to the app)
            CurrentAssembly ??= GetAssembly(type);
            context ??= CoreSourceGenerationContext.Default;
            string settings = Settings[sectionName].ToString();
            if (string.IsNullOrEmpty(settings))
                return default;
            return (T?)JsonSerializer.Deserialize(settings, typeof(T), context);
        }

        #endregion

        #region Static
#nullable enable
        public static Assembly? GetAssembly(Type type) => IntrospectionExtensions.GetTypeInfo(type)?.Assembly;
#nullable disable
        #endregion

        #region Extensions

        public string this[string name]
        {
            get
            {
                try
                {
                    string[] path = name.Split(':');
                    JsonElement node = _secrets.RootElement;
                    foreach (var segment in path)
                    {
                        if (node.TryGetProperty(segment, out var child))
                            node = child;
                        else
                            return string.Empty;
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

