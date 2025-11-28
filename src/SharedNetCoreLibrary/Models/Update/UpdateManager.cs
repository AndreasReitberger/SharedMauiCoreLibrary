using AndreasReitberger.Shared.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Update
{
    public abstract partial class UpdateManager : ObservableObject, IUpdateManager
    {
        #region Properties

        [ObservableProperty]
        public partial bool IsCheckingForUpdates { get; set; }
        #endregion

        #region Methods
        public abstract Task<bool> CheckForUpdate();
        #endregion
    }
}
