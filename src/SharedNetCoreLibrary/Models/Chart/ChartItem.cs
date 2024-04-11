using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Chart
{
    public partial class ChartItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        double value;
        #endregion

        #region Constructor
        public ChartItem() { }
        public ChartItem(string name, double value)
        {
            Name = name;
            Value = value;
        }
        #endregion
    }
}
