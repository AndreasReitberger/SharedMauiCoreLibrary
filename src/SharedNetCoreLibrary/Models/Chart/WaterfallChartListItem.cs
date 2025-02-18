using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Chart
{
    public partial class WaterfallChartListItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double Value { get; set; } = 0;

        [ObservableProperty]
        public partial bool IsSummary { get; set; } = false;
        #endregion
    }
}
