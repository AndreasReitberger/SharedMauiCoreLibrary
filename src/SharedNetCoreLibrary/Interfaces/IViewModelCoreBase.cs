using System.ComponentModel;

namespace AndreasReitberger.Shared.Core.Interfaces
{
    public interface IViewModelCoreBase : INotifyPropertyChanged
    {
        #region Properties
        IServiceProvider? Provider { get; set; }
        int IsBusyCounter { get; set; }
        bool IsBusy { get; set; }
        bool IsLoading { get; set; }
        bool IsReady { get; set; }
        bool IsStartUp { get; set; }
        bool IsRefreshing { get; set; }
        bool IsStartingUp { get; set; }
        bool IsBeta { get; set; }
        bool IsResuming { get; set; }
        bool IsPortrait { get; set; }

        #endregion
    }
}
