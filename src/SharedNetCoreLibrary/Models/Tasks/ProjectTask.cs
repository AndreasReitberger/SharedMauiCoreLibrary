using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Tasks
{
    public partial class ProjectTask : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string title = string.Empty;

        [ObservableProperty]
        DateTimeOffset dueDate;

        [ObservableProperty]
        bool isCompleted = false;

        [ObservableProperty]
        Guid projectId;
        #endregion
    }
}
