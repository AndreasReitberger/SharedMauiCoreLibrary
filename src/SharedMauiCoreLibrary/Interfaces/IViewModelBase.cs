using CommunityToolkit.Maui.Storage;

namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IViewModelBase : IViewModelCoreBase
    {
        #region Properties
        IDispatcher? Dispatcher { get; set; }
        IFileSaver? FileSaver { get; set; }
        ILauncher? Launcher { get; set; }
        #endregion
    }
}
