using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public partial class ProVersionFeature : ObservableObject
    {
        #region Propertiers
        [ObservableProperty]
        string feature = string.Empty;
        #endregion
    }
}
