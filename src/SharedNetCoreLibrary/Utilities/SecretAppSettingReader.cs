#if NEWTONSOFT
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
#endif

namespace AndreasReitberger.Shared.Core.Utilities
{
    public class SecretAppSettingReader
    {
        // Source: https://www.programmingwithwolfgang.com/use-net-secrets-in-console-application/
        public static T? ReadSection<T>(string sectionName, JsonSerializerContext? context = null)
        {
            // Needs the Directory.Build.targets in order to work (copies the secret.json as EmbeddedResource to the app)
            string settings = UserSecretsManager.Settings[sectionName].ToString();
            context ??= CoreSourceGenerationContext.Default;
            return (T?)JsonSerializer.Deserialize(settings, typeof(T), context);
        }

#if NEWTONSOFT
        public static T? ReadSectionN<T>(string sectionName)
        {
            // Needs the Directory.Build.targets in order to work (copies the secret.json as EmbeddedResource to the app)
            string settings = UserSecretsManager.Settings[sectionName].ToString();
            return JsonConvert.DeserializeObject<T>(settings);
        }
#endif
    }
}
