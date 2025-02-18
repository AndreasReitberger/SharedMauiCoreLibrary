namespace AndreasReitberger.Shared.Core
{
    public partial class ColorPickerElement : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Color? ChipColor { get; set; }
        #endregion
    }
}
