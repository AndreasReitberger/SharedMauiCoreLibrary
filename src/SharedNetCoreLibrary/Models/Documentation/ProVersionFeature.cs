using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public partial class ProVersionFeature : ObservableObject
    {
        #region Propertiers
        [ObservableProperty]
        public partial string Feature { get; set; } = string.Empty;
        #endregion
    }
}
