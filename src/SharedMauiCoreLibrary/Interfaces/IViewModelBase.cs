using CommunityToolkit.Maui.Storage;

namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IViewModelBase : IViewModelCoreBase
    {
        #region Properties
        IDispatcher? Dispatcher { get; set; }
        IFileSaver? FileSaver { get; set; }
        ILauncher? Launcher { get; set; }
        IFilePicker? FilePicker { get; set; }
        #endregion

        #region Methods
        void SetBusy(bool isBusy, IDispatcher? dispatcher);
        Task? SetBusyAsync(bool isBusy, IDispatcher? dispatcher);
        #endregion
    }
}
