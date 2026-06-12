using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace SharedMauiCoreLibrary.Test.Models
{
    public partial class TestModel : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool IsActive { get; set;  }

        [ObservableProperty]
        public partial DateTimeOffset Start { get; set; }

        [ObservableProperty]
        public partial DateTimeOffset End { get; set; }

        [ObservableProperty]
        public partial ObservableCollection<string> Items { get; set; } = [];

        #endregion
    }
}
