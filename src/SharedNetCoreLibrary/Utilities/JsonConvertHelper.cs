

#if NET6_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif

namespace AndreasReitberger.Shared.Core.Utilities
{
    public partial class JsonConvertHelper
    {
        #region Converts
        public static T? ToObject<T>(string? jsonString, T? defaultValue = default, Action<Exception>? OnError = null, JsonSerializerContext? context = null)
        {
            try
            {
                if (jsonString is null)
                    return defaultValue;
                context ??= CoreSourceGenerationContext.Default;
                // Check if it is saved as plain string. If so, just return the string
                if (typeof(T) == typeof(string) && !(jsonString.StartsWith('"') && jsonString.EndsWith('"')))
                    return (T)Convert.ChangeType(jsonString, typeof(T));
                else
                    return (T?)JsonSerializer.Deserialize(jsonString, typeof(T), context) ?? defaultValue;
            }
            catch (Exception exc)
            {
                OnError?.Invoke(exc);
                return defaultValue;
            }
        }

#if NET6_0_OR_GREATER
        [RequiresUnreferencedCode("This function is not AOT safe. Use the `JsonSerializerContext` instead")]
        [RequiresDynamicCode("This function is not AOT safe. Use the `JsonSerializerContext` instead")]
#endif
        public static T? ToObject<T>(string? jsonString, JsonSerializerOptions? options, T? defaultValue = default, Action<Exception>? OnError = null)
        {
            try
            {
                if (jsonString is null)
                    return defaultValue;
                options ??= CoreSourceGenerationContext.Default.Options;
                // Check if it is saved as plain string. If so, just return the string
                if (typeof(T) == typeof(string) && !(jsonString.StartsWith('"') && jsonString.EndsWith('"')))
                    return (T)Convert.ChangeType(jsonString, typeof(T));
                else
                    return JsonSerializer.Deserialize<T>(jsonString, options) ?? defaultValue;
            }
            catch (Exception exc)
            {
                OnError?.Invoke(exc);
                return defaultValue;
            }
        }

        public static string? ToSettingsString<T>(T? settingsObject, string? defaultValue = default, Action<Exception>? OnError = null, JsonSerializerContext? context = null)
        {
            try
            {
                if (settingsObject is null) return defaultValue;
                context ??= CoreSourceGenerationContext.Default;
                return JsonSerializer.Serialize(settingsObject, typeof(T), context) ?? defaultValue;
            }
            catch (Exception exc)
            {
                OnError?.Invoke(exc);
                return defaultValue;
            }
        }

#if NET6_0_OR_GREATER
        [RequiresUnreferencedCode("This function is not AOT safe. Use the `JsonSerializerContext` instead")]
        [RequiresDynamicCode("This function is not AOT safe. Use the `JsonSerializerContext` instead")]
#endif
        public static string? ToSettingsString<T>(T? settingsObject, JsonSerializerOptions? options, string? defaultValue = default, Action<Exception>? OnError = null)
        {
            try
            {
                if (settingsObject is null) return defaultValue;
                options ??= CoreSourceGenerationContext.Default.Options;
                return JsonSerializer.Serialize(settingsObject, options) ?? defaultValue;
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