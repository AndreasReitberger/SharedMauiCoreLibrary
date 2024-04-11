using AndreasReitberger.Shared.Core.Interfaces;
using AndreasReitberger.Shared.Core.Events;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Localization
{
    public partial class LocalizationManager : ObservableObject, ILocalizationManager
    {
        #region Instance
        static LocalizationManager? _instance = null;
        static readonly object Lock = new();
        public static LocalizationManager? Instance
        {
            get
            {
                lock (Lock)
                {
                    _instance ??= new();
                }
                return _instance;
            }
            set
            {
                if (_instance == value) return;
                lock (Lock)
                {
                    _instance = value;
                }
            }

        }
        #endregion

        #region Variables
        const string _defaultCultureCode = "en-US";
        //const string _baseFlagImageUri = @"";
        #endregion

        #region Properties
        [ObservableProperty]
        string baseFlagImageUri = string.Empty;
        [ObservableProperty]
        List<LocalizationInfo> languages = [];
        [ObservableProperty]
        LocalizationInfo currentLanguage = new();
        [ObservableProperty]
        CultureInfo? currentCulture;
        #endregion

        #region Constructor
        public LocalizationManager() { }
        #endregion

        #region Events

        public event EventHandler? LanguageChanged;
        protected virtual void OnLanguageChanged(LanguageChangedEventArgs e)
        {
            LanguageChanged?.Invoke(this, e);
        }
        #endregion

        #region Methods
        public void InitialLanguage(string cultureCode = "")
        {
            if (string.IsNullOrEmpty(cultureCode))
            {
                cultureCode = CultureInfo.CurrentCulture.Name;
            }
            LocalizationInfo? info = GetLocalizationInfoBasedOnCode(cultureCode) ?? Languages.FirstOrDefault();
            if (info?.Code != Languages.First().Code)
            {
                Change(info);
            }
            else
            {
                CurrentLanguage = info;
                CurrentCulture = new CultureInfo(info.Code);
            }
        }

        public void SetLanguages(List<LocalizationInfo> languages) => Languages = languages ?? [];

        public LocalizationInfo? GetLocalizationInfoBasedOnCode(string cultureCode) => Languages?.FirstOrDefault(x => x.Code == cultureCode) ?? null;

        public Uri GetImageUri(string cultureCode)
        {
            Uri image = string.IsNullOrEmpty(BaseFlagImageUri) ?
                 new($"{cultureCode.Replace("-", "_").ToLowerInvariant()}.png", UriKind.RelativeOrAbsolute) :
                 new($"{BaseFlagImageUri}/{cultureCode.Replace("-", "_").ToLowerInvariant()}.png", UriKind.RelativeOrAbsolute);
            return image;
        }

        public static Uri GetImageUri(string baseFlagUri, string cultureCode)
        {
            Uri image = string.IsNullOrEmpty(baseFlagUri) ?
                 new($"{cultureCode.Replace("-", "_").ToLowerInvariant()}.png", UriKind.RelativeOrAbsolute) :
                 new($"{baseFlagUri}/{cultureCode.Replace("-", "_").ToLowerInvariant()}.png", UriKind.RelativeOrAbsolute);
            return image;
        }

        public void Change(LocalizationInfo? info)
        {
            CurrentLanguage = info ?? new(_defaultCultureCode);
            CurrentCulture = new CultureInfo(CurrentLanguage.Code);
            OnLanguageChanged(new()
            {
                LangaugeInfo = CurrentLanguage,
                LangaugeCode = CurrentLanguage.Code,
                Culture = CurrentCulture,
            });
        }

        public void Change(LocalizationInfo info, Action<LocalizationInfo> action)
        {
            action?.Invoke(info);
            OnLanguageChanged(new()
            {
                LangaugeInfo = info,
                LangaugeCode = info.Code,
                Culture = CurrentCulture,
            });
        }

        public bool Change(LocalizationInfo info, Func<LocalizationInfo, bool> function)
        {
            OnLanguageChanged(new()
            {
                LangaugeInfo = info,
                LangaugeCode = info.Code,
                Culture = CurrentCulture,
            });
            return function?.Invoke(info) ?? false;
        }
        #endregion

    }
}
