using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Localization
{
    public partial class LocalizationInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string NativeName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Uri? FlagUri { get; set; }

        [ObservableProperty]
        public partial string Translator { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Code { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double PercentTranslated { get; set; } = 0;

        [ObservableProperty]
        public partial bool IsOfficial { get; set; } = false;
        #endregion

        #region Constructors
        public LocalizationInfo() { }

        public LocalizationInfo(string code)
        {
            Code = code;
        }

        public LocalizationInfo(string name, string nativeName, Uri flagUri, string translator, string code, double percentTranslated, bool isOfficial)
        {
            Name = name;
            NativeName = nativeName;
            FlagUri = flagUri;
            Translator = translator;
            Code = code;
            PercentTranslated = percentTranslated;
            IsOfficial = isOfficial;
        }
        #endregion

        #region Overrides

        public override string ToString() => JsonSerializer.Serialize(this!, CoreSourceGenerationContext.Default.LocalizationInfo);

        #endregion
    }
}
