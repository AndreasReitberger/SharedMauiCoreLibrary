using AndreasReitberger.Shared.Core.Utilities;
using Newtonsoft.Json;

namespace SharedMauiCoreLibrary.Test.Utilities
{
    public static class SecretAppSettingReaderExtension
    {
        // Source: https://www.programmingwithwolfgang.com/use-net-secrets-in-console-application/
        public static T? ReadSectionFromConfigurationRoot<T>(this SecretAppSettingReader secretAppSettingReader, string sectionName)
        {

            // Needs the Directory.Build.targets in order to work (copies the secret.json as EmbeddedResource to the app)
            string settings = UserSecretsManager.Settings?[sectionName]?.ToString() ?? string.Empty;
            return JsonConvert.DeserializeObject<T?>(settings);
        }
    }
}
