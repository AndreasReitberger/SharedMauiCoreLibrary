using Newtonsoft.Json;
using System.Globalization;
using AndreasReitberger.Shared.Core.Localization;

namespace AndreasReitberger.Shared.Core.Events
{
    public class LanguageChangedEventArgs : EventArgs
    {
        #region Properties
        public string Message { get; set; }
        public LocalizationInfo LangaugeInfo { get; set; }
        public CultureInfo Culture { get; set; }
        public string LangaugeCode { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}
