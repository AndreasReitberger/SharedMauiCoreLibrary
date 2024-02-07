namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IViewModelBase : IViewModelCoreBase
    {
        #region Properties
        IDispatcher Dispatcher { get; set; }
        #endregion
    }
}
