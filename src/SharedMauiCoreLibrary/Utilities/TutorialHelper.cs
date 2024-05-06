namespace AndreasReitberger.Shared.Core.Utilities
{
    public static class TutorialHelper
    {
        #region Methods
        public static string GetTutorialImageSourceString(AppTheme? theme, string baseDirectory, int order, string fileType = "png", string prefix = "")
        {
            theme ??= AppTheme.Unspecified;
            return theme switch
            {
                AppTheme.Dark => $"{baseDirectory}/{prefix}{DeviceInfo.Platform.ToString().ToLowerInvariant()}_dark_{order}.{fileType}",
                _ => $"{baseDirectory}/{prefix}{DeviceInfo.Platform.ToString().ToLowerInvariant()}_light_{order}.{fileType}",
            };
        }
        public static string GetTutorialImageSourceString(AppTheme? theme, int order, string fileType = "png", string prefix = "")
        {
            theme ??= AppTheme.Unspecified;
            return theme switch
            {
                AppTheme.Dark => $"{prefix}{DeviceInfo.Platform.ToString().ToLowerInvariant()}_dark_{order}.{fileType}",
                _ => $"{prefix}{DeviceInfo.Platform.ToString().ToLowerInvariant()}_light_{order}.{fileType}",
            };
        }
        #endregion
    }
}
