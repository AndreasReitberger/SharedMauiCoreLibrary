#if NEWTONSOFT
using Newtonsoft.Json;
#else
using System.Text.Json;
#endif

namespace AndreasReitberger.Shared.Core.Utilities
{
    public partial class JsonConvertHelper
    {
        #region Converts
#if NEWTONSOFT
        public static T? ToObject<T>(string jsonString, T? defaultValue = default, Action<Exception>? OnError = null, JsonSerializerSettings? settings = null)
#else
        public static T? ToObject<T>(string jsonString, T? defaultValue = default, Action<Exception>? OnError = null, JsonSerializerOptions? settings = null)
#endif
        {
            try
            {
#if NEWTONSOFT
                settings ??= new JsonSerializerSettings()
                {
                    Error = (a, b) =>
                    {
                        throw new JsonReaderException($"Exception while deserializing the object type `{typeof(T)}` from the string `{jsonString}");
                    }
                };
#else
                settings ??= new JsonSerializerOptions();
#endif
                // Check if it is saved as plain string. If so, just return the string
                if (typeof(T) == typeof(string) && !(jsonString.StartsWith('"') && jsonString.EndsWith('"')))
                    return (T)Convert.ChangeType(jsonString, typeof(T));
                else
#if NEWTONSOFT
                    return JsonConvert.DeserializeObject<T>(jsonString, settings) ?? defaultValue;
#else
                    return JsonSerializer.Deserialize<T>(jsonString, settings) ?? defaultValue;
#endif
            }
            catch (Exception exc)
            {
                OnError?.Invoke(exc);
                return defaultValue;
            }
        }
#if NEWTONSOFT
        public static string? ToSettingsString<T>(T settingsObject, string? defaultValue = default, Action<Exception>? OnError = null, JsonSerializerSettings? settings = null)
#else
        public static string? ToSettingsString<T>(T settingsObject, string? defaultValue = default, Action<Exception>? OnError = null, JsonSerializerOptions? settings = null)
#endif
        {
            try
            {
#if NEWTONSOFT
                settings ??= new JsonSerializerSettings()
                {
                    Error = (a, b) =>
                    {
                        throw new JsonReaderException($"Exception while serializing the object type `{typeof(T)}`.");
                    }
                };
                return JsonConvert.SerializeObject(settingsObject, Formatting.Indented, settings) ?? defaultValue;
#else
                settings ??= new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                return JsonSerializer.Serialize(settingsObject, settings) ?? defaultValue;
#endif
            }
            catch (Exception exc)
            {
                OnError?.Invoke(exc);
                return defaultValue;
            }
        }
#endregion
    }
}
