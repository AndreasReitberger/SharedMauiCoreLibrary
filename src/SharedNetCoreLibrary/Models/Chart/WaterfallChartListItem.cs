namespace AndreasReitberger.Shared.Core.Chart
{
    public class WaterfallChartListItem
    {
        #region Properties
        public string Name { get; set; }
        public double Value { get; set; }
        public bool IsSummary { get; set; } = false;
        #endregion
    }
}
