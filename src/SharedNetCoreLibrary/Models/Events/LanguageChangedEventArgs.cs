#if NEWTONSOFT
using Newtonsoft.Json;
#else
using System.Text.Json;
#endif
using System.Globalization;
using AndreasReitberger.Shared.Core.Localization;

namespace AndreasReitberger.Shared.Core.Events
{
    public class LanguageChangedEventArgs : EventArgs
    {
        #region Properties
        public string Message { get; set; } = string.Empty;
        public LocalizationInfo? LangaugeInfo { get; set; }
        public CultureInfo? Culture { get; set; }
        public string LangaugeCode { get; set; } = string.Empty;
        #endregion

        #region Overrides

#if NEWTONSOFT
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
#else
        public override string ToString() => JsonSerializer.Serialize(this!, CoreSourceGenerationContext.Default.LanguageChangedEventArgs);
#endif
        #endregion
    }
}
