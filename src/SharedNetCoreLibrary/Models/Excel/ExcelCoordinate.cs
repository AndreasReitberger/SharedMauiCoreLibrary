namespace AndreasReitberger.Shared.Core.Models.Excel
{
    public partial class ExcelCoordinate : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public partial string Column { get; set; } = "A";

        [ObservableProperty]
        public partial int Row { get; set; } = 0;
        #endregion

        #region Constructor
        public ExcelCoordinate() { }
        #endregion

        #region Overrides
        public override string ToString() => $"{Column}:{Row}";

        #endregion
    }
}
