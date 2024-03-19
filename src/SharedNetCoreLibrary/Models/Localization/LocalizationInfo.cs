using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Localization
{
    public partial class LocalizationInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        string nativeName = string.Empty;

        [ObservableProperty]
        Uri? flagUri;

        [ObservableProperty]
        string translator = string.Empty;

        [ObservableProperty]
        string code = string.Empty;

        [ObservableProperty]
        double percentTranslated = 0;

        [ObservableProperty]
        bool isOfficial = false;
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
    }
}
