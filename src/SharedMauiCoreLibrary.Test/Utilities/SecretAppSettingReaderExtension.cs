using AndreasReitberger.Shared.Core.Utilities;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SharedMauiCoreLibrary.Test.Utilities
{
    public static class SecretAppSettingReaderExtension
    {
        /*
        // Source: https://www.programmingwithwolfgang.com/use-net-secrets-in-console-application/
        public static T? ReadSectionFromConfigurationRoot<T>(this SecretAppSettingReader secretAppSettingReader, string sectionName)
        {

            // Needs the Directory.Build.targets in order to work (copies the secret.json as EmbeddedResource to the app)
            string settings = UserSecretsManager.Settings?[sectionName]?.ToString() ?? string.Empty;
            return JsonConvert.DeserializeObject<T?>(settings);
        }
        */

        public static Assembly? Assembly { get; set; }

        /// <summary>
        /// Source: https://www.programmingwithwolfgang.com/use-net-secrets-in-console-application/ 
        /// </summary>
        /// <typeparam name="T">The <c>Type</c> for parsing the <c>secrets.json</c> to</typeparam>
        /// <param name="sectionName">The section name</param>
        /// <returns></returns>
        public static T? ReadSectionFromConfigurationRoot<T>(string sectionName, JsonSerializerContext? context = null)
        {
            // It seems that this way makes problems if the app is published on Windows in Release mode
            // Needs the Directory.Build.targets in order to work (copies the secret.json as EmbeddedResource to the app)
            if (Assembly is null)
            {
                Assembly = IntrospectionExtensions.GetTypeInfo(typeof(SecretAppSettingReaderExtension)).Assembly;
                UserSecretsManager.Settings = new UserSecretsManager.UserSecretsManagerBuilder()
                    .WithAppNamespace("SharedMauiCoreLibrary.Test")
                    .WithCustomAssambly(Assembly)
                    .Build();
            }
            string settings = UserSecretsManager.Settings[sectionName].ToString();
            if (string.IsNullOrEmpty(settings))
                return default;
            return (T?)JsonSerializer.Deserialize(settings, typeof(T));
        }
    }
}
