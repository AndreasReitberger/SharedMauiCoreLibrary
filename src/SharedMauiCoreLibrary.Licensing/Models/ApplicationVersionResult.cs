using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing
{
    public partial class ApplicationVersionResult : ObservableObject, IApplicationVersionResult
    {
        #region Properties

        [ObservableProperty]
        public partial bool Success { get; set; } = false;

        [ObservableProperty]
        public partial DateTimeOffset TimeStamp { get; set; }

        [ObservableProperty]
        public partial string Version { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Message { get; set; } = string.Empty;

        #endregion
    }
}
