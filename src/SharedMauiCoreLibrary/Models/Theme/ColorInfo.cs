﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Theme
{
    public partial class ColorInfo : ObservableObject
    {
        #region Properties
        /// <summary>
        /// A matching name for the color information.
        /// </summary>
        [ObservableProperty]
        string name = string.Empty;

        /// <summary>
        /// The color for this info.
        /// </summary>
        [ObservableProperty]
        Color? color;
        partial void OnColorChanged(Color? value)
        {
            if (string.IsNullOrEmpty(Name))
                Name = value?.ToArgbHex() ?? string.Empty;
        }

        #endregion

        #region Ctor
        public ColorInfo() { }
        public ColorInfo(string colorName, Color color)
        {
            Name = colorName;
            Color = color;
        }
        #endregion
    }
}
