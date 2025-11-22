using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Firebase
{
    public partial class FirebaseHandler : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public partial bool EnableEncryption { get; set; } = true;
        #endregion
    }
}
