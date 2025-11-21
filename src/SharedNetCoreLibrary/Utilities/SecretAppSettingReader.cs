#if NEWTONSOFT
using Newtonsoft.Json;
#else
using System.Text.Json;
#endif

namespace AndreasReitberger.Shared.Core.Utilities
{
    public class SecretAppSettingReader
    {
        // Source: https://www.programmingwithwolfgang.com/use-net-secrets-in-console-application/
        public static T? ReadSection<T>(string sectionName)
        {
            // Needs the Directory.Build.targets in order to work (copies the secret.json as EmbeddedResource to the app)
            string settings = UserSecretsManager.Settings[sectionName].ToString();
#if NEWTONSOFT
            return JsonConvert.DeserializeObject<T>(settings);
#else
            return JsonSerializer.Deserialize<T>(settings);
#endif
        }
    }
}
