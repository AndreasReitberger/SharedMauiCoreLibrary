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

        public override string ToString() => JsonSerializer.Serialize(this!, CoreSourceGenerationContext.Default.LanguageChangedEventArgs);

        #endregion
    }
}
