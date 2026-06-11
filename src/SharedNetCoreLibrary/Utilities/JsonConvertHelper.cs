
namespace AndreasReitberger.Shared.Core.Utilities
{
    public partial class JsonConvertHelper
    {
        #region Converts
        public static T? ToObject<T>(string? jsonString, T? defaultValue = default, Action<Exception>? OnError = null, JsonSerializerContext? settings = null)
        {
            try
            {
                if (jsonString is null)
                    return defaultValue;
                settings ??= CoreSourceGenerationContext.Default;
                // Check if it is saved as plain string. If so, just return the string
                if (typeof(T) == typeof(string) && !(jsonString.StartsWith('"') && jsonString.EndsWith('"')))
                    return (T)Convert.ChangeType(jsonString, typeof(T));
                else
                    return (T?)JsonSerializer.Deserialize(jsonString, typeof(T), settings) ?? defaultValue;
            }
            catch (Exception exc)
            {
                OnError?.Invoke(exc);
                return defaultValue;
            }
        }

        public static string? ToSettingsString<T>(T? settingsObject, string? defaultValue = default, Action<Exception>? OnError = null, JsonSerializerContext? settings = null)
        {
            try
            {
                if (settingsObject is null) return defaultValue;
                settings ??= CoreSourceGenerationContext.Default;
                return JsonSerializer.Serialize(settingsObject, typeof(T), settings) ?? defaultValue;
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