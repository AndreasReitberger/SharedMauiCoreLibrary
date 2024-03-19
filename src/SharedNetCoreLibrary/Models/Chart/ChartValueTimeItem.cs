using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Chart
{
    public partial class ChartValueTimeItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        double value = 0;

        [ObservableProperty]
        int time = 0;
        #endregion
    }
}
