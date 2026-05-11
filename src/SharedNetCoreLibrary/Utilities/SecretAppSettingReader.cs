#if NEWTONSOFT
using Newtonsoft.Json;
#endif
using System.Reflection;

namespace AndreasReitberger.Shared.Core.Utilities
{
    [Obsolete("Use `UserSecretsManager` instead of this class")]
    public class SecretAppSettingReader
    {
        #region Properties
        public static Assembly? Assembly { get; set; }
        #endregion

        // Source: https://www.programmingwithwolfgang.com/use-net-secrets-in-console-application/
#if NEWTONSOFT
        public static T? ReadSection<T>(string sectionName)
#else
        public static T? ReadSection<T>(string sectionName, JsonSerializerContext? context = null)
#endif
        {
            // Needs the Directory.Build.targets in order to work (copies the secret.json as EmbeddedResource to the app)
            string settings = UserSecretsManager.Settings[sectionName].ToString();
#if NEWTONSOFT
            return JsonConvert.DeserializeObject<T>(settings);
#else
            context ??= CoreSourceGenerationContext.Default;
            return (T?)JsonSerializer.Deserialize(settings, typeof(T), context);
#endif
        }

#if !NEWTONSOFT
        public static T? ReadSectionFromConfigurationRoot<T>(Type type, string appNameSpace, string sectionName, JsonSerializerContext? context = null)
        {
            // It seems that this way makes problems if the app is published on Windows in Release mode
            // Needs the Directory.Build.targets in order to work (copies the secret.json as EmbeddedResource to the app)
            if (Assembly is null)
            {
                Assembly = IntrospectionExtensions.GetTypeInfo(type).Assembly;
                UserSecretsManager.Settings = new UserSecretsManager.UserSecretsManagerBuilder()
                    .WithAppNamespace(appNameSpace)
                    .WithCustomAssambly(Assembly)
                    .Build();
            }
            context ??= CoreSourceGenerationContext.Default;
            string settings = UserSecretsManager.Settings[sectionName].ToString();
            if (string.IsNullOrEmpty(settings))
                return default;
            return (T?)JsonSerializer.Deserialize(settings, typeof(T), context);
        }
    }
#endif
}
