#if NEWTONSOFT
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
#endif
namespace AndreasReitberger.Shared.Core.Utilities
{
    public partial class JsonConvertHelper
    {
        #region Converts
        /// <summary>
        /// Deserializes the specified JSON string to an object of type T using System.Text.JSON.
        /// Returns a default value if deserialization fails.
        /// </summary>
        /// <remarks>If the input JSON string is not a valid JSON representation of type T, or if an
        /// exception occurs during deserialization, the method returns the specified default value and optionally
        /// invokes the OnError callback. When T is string and the input is not a quoted JSON string, the method returns
        /// the raw string value.</remarks>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="jsonString">The JSON string to deserialize. If T is string and the input is not a quoted JSON string, the raw value is
        /// returned as a string.</param>
        /// <param name="defaultValue">The value to return if deserialization fails or the result is null. The default is the default value of type
        /// T.</param>
        /// <param name="OnError">An optional callback that is invoked with the exception if an error occurs during deserialization.</param>
        /// <param name="settings">An optional JsonSerializerContext to use for deserialization. If null, a default context is used.</param>
        /// <returns>An object of type T deserialized from the JSON string, or the specified default value if deserialization
        /// fails.</returns>
        public static T? ToObject<T>(string jsonString, T? defaultValue = default, Action<Exception>? OnError = null, JsonSerializerContext? settings = null)
        {
            try
            {
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

#if NEWTONSOFT
        public static T? ToObjectN<T>(string jsonString, T? defaultValue = default, Action<Exception>? OnError = null, JsonSerializerSettings? settings = null)
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
                if (typeof(T) == typeof(string) && !(jsonString.StartsWith('"') && jsonString.EndsWith('"')))
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
#endif

        /// <summary>
        /// Serializes the specified settings object to a JSON string representation using System.Text.JSON.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="settingsObject">The object to serialize to a JSON string. Can be null.</param>
        /// <param name="defaultValue">The value to return if serialization fails or results in null. The default is null.</param>
        /// <param name="OnError">An optional callback that is invoked if an exception occurs during serialization. Can be null.</param>
        /// <param name="settings">The optional source generation context to use for serialization. If null, the default context is used.</param>
        /// <returns>A JSON string representation of the settings object, or the specified default value if serialization fails.</returns>
        public static string? ToSettingsString<T>(T settingsObject, string? defaultValue = default, Action<Exception>? OnError = null, JsonSerializerContext? settings = null)
        {
            try
            {
                settings ??= CoreSourceGenerationContext.Default;
                return JsonSerializer.Serialize(settingsObject, typeof(T), settings) ?? defaultValue;
            }
            catch (Exception exc)
            {
                OnError?.Invoke(exc);
                return defaultValue;
            }
        }

#if NEWTONSOFT
        public static string? ToSettingsStringN<T>(T settingsObject, string? defaultValue = default, Action<Exception>? OnError = null, JsonSerializerSettings? settings = null)
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
#endif
        #endregion
    }
}