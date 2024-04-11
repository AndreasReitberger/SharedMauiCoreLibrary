using Newtonsoft.Json;

namespace AndreasReitberger.Shared.Core.Utilities
{
    public partial class JsonConvertHelper
    {
        #region Converts
        public static T? ToObject<T>(string jsonString, T? defaultValue = default, Action<Exception>? OnError = null, JsonSerializerSettings? settings = null)
        {
            try
            {
                settings ??= new JsonSerializerSettings()
                {
                    Error = (a, b) =>
                    {
                        throw new JsonReaderException($"Exception while deserializing the object type `{typeof(T)}` from the string `{jsonString}");
                    }
                };
                // Check if it is saved as plain string. If so, just return the string
                if (typeof(T) == typeof(string) && !(jsonString.StartsWith("\"") && jsonString.EndsWith("\"")))
                    return (T)Convert.ChangeType(jsonString, typeof(T));
                else
                    return JsonConvert.DeserializeObject<T>(jsonString, settings) ?? defaultValue;
            }
            catch (Exception exc)
            {
                OnError?.Invoke(exc);
                return defaultValue;
            }
        }
        public static string? ToSettingsString<T>(T settingsObject, string? defaultValue = default, Action<Exception>? OnError = null, JsonSerializerSettings? settings = null)
        {
            try
            {
                settings ??= new JsonSerializerSettings()
                {
                    Error = (a, b) =>
                    {
                        throw new JsonReaderException($"Exception while serializing the object type `{typeof(T)}`.");
                    }
                };
                return JsonConvert.SerializeObject(settingsObject, Formatting.Indented, settings) ?? defaultValue;
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
