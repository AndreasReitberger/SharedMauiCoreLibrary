using Newtonsoft.Json;

namespace AndreasReitberger.Shared.Core.Utilities
{
    public partial class JsonConvertHelper
    {
        #region Converts
        public static T ToObject<T>(string serversSettings, T defaultValue = default, Action<Exception> OnError = null)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(serversSettings) ?? defaultValue;
            }
            catch (Exception exc)
            {
                OnError?.Invoke(exc);
                return defaultValue;
            }
        }
        public static string ToSettingsString<T>(T settingsObject, string defaultValue = default, Action<Exception> OnError = null)
        {
            try
            {
                return JsonConvert.SerializeObject(settingsObject) ?? defaultValue;
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
