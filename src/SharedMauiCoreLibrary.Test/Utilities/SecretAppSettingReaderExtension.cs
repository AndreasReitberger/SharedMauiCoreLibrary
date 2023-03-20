using AndreasReitberger.Shared.Core.Utilities;
using Microsoft.Extensions.Configuration;

namespace SharedMauiCoreLibrary.Test.Utilities
{
    public static class SecretAppSettingReaderExtension
    {
        // Source: https://www.programmingwithwolfgang.com/use-net-secrets-in-console-application/
        public static T ReadSectionFromConfigurationRoot<T>(this SecretAppSettingReader secretAppSettingReader, string sectionName)
        {

            // This only works on Windows, otherwise the secret.json file is not found
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddUserSecrets<Tests>()
            ;
            IConfigurationRoot configurationRoot = builder.Build();
            return configurationRoot.GetSection(sectionName).Get<T>();
#if false
            // Needs the Directory.Build.targets in order to work (copies the secret.json as EmbeddedResource to the app)
            string settings = UserSecretsManager.Settings[sectionName].ToString();
            return JsonConvert.DeserializeObject<T>(settings);
#endif
        }
    }
}
