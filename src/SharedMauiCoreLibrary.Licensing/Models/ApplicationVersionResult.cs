using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing
{
    public partial class ApplicationVersionResult : ObservableObject, IApplicationVersionResult
    {
        #region Properties

        [ObservableProperty]
        bool success;

        [ObservableProperty]
        DateTimeOffset timeStamp;

        [ObservableProperty]
        string version;

        [ObservableProperty]
        string message;

        #endregion
    }
}
