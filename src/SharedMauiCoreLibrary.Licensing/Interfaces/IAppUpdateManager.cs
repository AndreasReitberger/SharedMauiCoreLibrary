using AndreasReitberger.Shared.Core.Interfaces;

namespace AndreasReitberger.Shared.Core.Licensing.Interfaces
{
    public interface IAppUpdateManager : IUpdateManager
    {
        #region Properties
        public IDispatcher? Dispatcher { get; set; }
        public ILicenseManager? LicenseManager { get; set; }
        #endregion
    }
}
