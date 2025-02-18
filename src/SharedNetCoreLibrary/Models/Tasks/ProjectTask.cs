using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Tasks
{
    public partial class ProjectTask : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Title { get; set; } = string.Empty;

        [ObservableProperty]
        public partial DateTimeOffset DueDate { get; set; }

        [ObservableProperty]
        public partial bool IsCompleted { get; set; } = false;

        [ObservableProperty]
        public partial Guid ProjectId { get; set; }
        #endregion
    }
}
