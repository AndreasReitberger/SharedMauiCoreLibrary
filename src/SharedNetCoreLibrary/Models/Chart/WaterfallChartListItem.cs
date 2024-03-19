using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Chart
{
    public partial class WaterfallChartListItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        double value = 0;

        [ObservableProperty]
        bool isSummary = false;
        #endregion
    }
}
