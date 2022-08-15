using AndreasReitberger.Shared.Core.Interfaces;
using System.Globalization;

namespace AndreasReitberger.Shared.Core.Localization
{
    public partial class LocalizationManager : ILocalizationManager
    {
        #region Instance
        static LocalizationManager _instance = null;
        static readonly object Lock = new();
        public static LocalizationManager Instance
        {
            get
            {
                lock (Lock)
                {
                    if (_instance == null)
                        _instance = new();
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
        public string BaseFlagImageUri { get; set; } = "";
        public List<LocalizationInfo> Languages { get; set; } = new();
        public LocalizationInfo CurrentLanguage { get; set; } = new();
        public CultureInfo CurrentCulture { get; set; }
        #endregion

        #region Constructor
        public LocalizationManager()
        {

        }
        #endregion

        #region Methods
        public void InitialLanguage(string cultureCode = "")
        {
            if (string.IsNullOrEmpty(cultureCode))
            {
                cultureCode = CultureInfo.CurrentCulture.Name;
            }
            LocalizationInfo info = GetLocalizationInfoBasedOnCode(cultureCode) ?? Languages.FirstOrDefault();
            if (info.Code != Languages.First().Code)
            {
                Change(info);
            }
            else
            {
                CurrentLanguage = info;
                CurrentCulture = new CultureInfo(info.Code);
            }
        }

        public void SetLanguages(List<LocalizationInfo> languages)
        {
            Languages = languages ?? new();
        }
        public LocalizationInfo GetLocalizationInfoBasedOnCode(string cultureCode)
        {
            return Languages.FirstOrDefault(x => x.Code == cultureCode) ?? null;
        }

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

        public void Change(LocalizationInfo info)
        {
            CurrentLanguage = info;
            CurrentCulture = new CultureInfo(info.Code);
        }

        public void Change(LocalizationInfo info, Action<LocalizationInfo> action)
        {
            action?.Invoke(info);
        }

        public bool Change(LocalizationInfo info, Func<LocalizationInfo, bool> function)
        {
            return function?.Invoke(info) ?? false;
        }
        #endregion

    }
}
