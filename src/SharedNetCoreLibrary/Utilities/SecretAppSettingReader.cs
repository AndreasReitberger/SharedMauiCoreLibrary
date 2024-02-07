#if WINDOWS && false
using Microsoft.Extensions.Configuration;
#else
using Newtonsoft.Json;
#endif

namespace AndreasReitberger.Shared.Core.Utilities
{
    public class SecretAppSettingReader
    {
        // Source: https://www.programmingwithwolfgang.com/use-net-secrets-in-console-application/
        public static T ReadSection<T>(string sectionName)
        {

#if WINDOWS && false
            // This only works on Windows, otherwise the secret.json file is not found
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddUserSecrets<typeof(app)>()
            ;
            IConfigurationRoot configurationRoot = builder.Build();          
            return configurationRoot.GetSection(sectionName).Get<T>();
#else
            // Needs the Directory.Build.targets in order to work (copies the secret.json as EmbeddedResource to the app)
            string settings = UserSecretsManager.Settings[sectionName].ToString();
            return JsonConvert.DeserializeObject<T>(settings);
#endif
        }
    }
}
