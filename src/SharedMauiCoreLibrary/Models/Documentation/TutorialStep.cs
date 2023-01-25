using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Documentation
{
    public partial class TutorialStep : ObservableObject
    {
        #region Properties
        Uri image;
        public Uri Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        string heading;
        public string Heading
        {
            get => heading;
            set => SetProperty(ref heading, value);
        }

        string content;
        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }

        int order = 0;
        public int Order
        {
            get => order;
            set => SetProperty(ref order, value);
        }

        bool viewed = false;
        public bool Viewed
        {
            get => viewed;
            set => SetProperty(ref viewed, value);
        }
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
