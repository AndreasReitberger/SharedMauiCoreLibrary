#if NEWTONSOFT
using Newtonsoft.Json;
#endif

namespace AndreasReitberger.Shared.Core.Utilities
{
    public class SecretAppSettingReader
    {
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
    }
}
