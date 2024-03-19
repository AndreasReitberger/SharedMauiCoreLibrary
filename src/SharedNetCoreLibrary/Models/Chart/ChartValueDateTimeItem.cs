using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Chart
{
    public partial class ChartValueDateTimeItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        double value = 0;

        [ObservableProperty]
        DateTime? time;
        #endregion
    }
}
