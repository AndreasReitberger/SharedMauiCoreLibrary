using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public partial class TutorialStep : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public partial Uri? Image { get; set; }

        [ObservableProperty]
        public partial string Heading { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Content { get; set; } = string.Empty;

        [ObservableProperty]
        public partial int Order { get; set; } = 0;

        [ObservableProperty]
        public partial bool Viewed { get; set; } = false;

        #endregion

        #region Constructor
        public TutorialStep() { }
        public TutorialStep(string heading, string content, int order = 0)
        {
            Heading = heading;
            Content = content;
            Order = order;
        }
        public TutorialStep(string heading, string content, Uri image, int order = 0)
        {
            Heading = heading;
            Content = content;
            Order = order;
            Image = image;
        }
        #endregion
    }
}
