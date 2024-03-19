using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public partial class TutorialStep : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        Uri? image;

        [ObservableProperty]
        string heading = string.Empty;

        [ObservableProperty]
        string content = string.Empty;

        [ObservableProperty]
        int order = 0;

        [ObservableProperty]
        bool viewed = false;

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
