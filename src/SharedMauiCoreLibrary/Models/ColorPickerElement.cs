using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core
{
    public partial class ColorPickerElement : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        Color? chipColor;
        #endregion
    }
}
