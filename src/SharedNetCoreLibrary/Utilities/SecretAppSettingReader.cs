using System.Reflection;

namespace AndreasReitberger.Shared.Core.Utilities
{
    [Obsolete("Use `UserSecretsManager` instead of this class")]
    public class SecretAppSettingReader
    {
        #region Properties
        public static Assembly? Assembly { get; set; }
        #endregion

        public static T? ReadSectionFromConfigurationRoot<T>(Type type, string appNameSpace, string sectionName, JsonSerializerContext? context = null)
        {
            // It seems that this way makes problems if the app is published on Windows in Release mode
            // Needs the Directory.Build.targets in order to work (copies the secret.json as EmbeddedResource to the app)
            UserSecretsManager? secrets = null;
            if (Assembly is null)
            {
                Assembly = IntrospectionExtensions.GetTypeInfo(type).Assembly;
                secrets = new UserSecretsManager.UserSecretsManagerBuilder()
                    .WithAppNamespace(appNameSpace)
                    .WithCustomAssambly(Assembly)
                    .Build();
            }
            context ??= CoreSourceGenerationContext.Default;
            string? settings = secrets?[sectionName].ToString();
            if (string.IsNullOrEmpty(settings))
                return default;
            return (T?)JsonSerializer.Deserialize(settings, typeof(T), context);
        }
    }
}
