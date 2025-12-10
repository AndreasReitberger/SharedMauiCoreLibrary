namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IViewModelBase : IViewModelCoreBase
    {
        #region Properties
        IDispatcher? Dispatcher { get; set; }
        #endregion

        #region Methods
        void SetBusy(bool isBusy, IDispatcher? dispatcher);
        Task? SetBusyAsync(bool isBusy, IDispatcher? dispatcher);
        Task? SetBusyAsync(bool isBusy);
        #endregion
    }
}
