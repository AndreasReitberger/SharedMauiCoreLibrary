using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Shared.Core.Licensing.Envato
{
    /// <summary>
    /// Represents an activation response from Envato:
    /// ==============================================
    /// "activated": true
    ///"instance": 1473192358
    ///"message": "2 out of 5 activations remaining"
    ///"timestamp": 1473192358
    ///"errorcode": 101
    ///"errrormessage": Invalid license key...
    ///"sig": "secret=null; activated=true; instance=1473192358; message=2 out of 5 activations remaining; timestamp=1473192358"
    /// </summary>
    public partial class EnvatoActivationResponse : ObservableObject, IActivationResponse
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("activated")]
        public partial bool Activated { get; set; } = false;

        [ObservableProperty]
        [JsonPropertyName("instance")]
        public partial int Instance { get; set; }

        [ObservableProperty]
        [JsonPropertyName("message")]
        public partial string Message { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("timestamp")]
        public partial int Timestamp { get; set; }

        [ObservableProperty]
        [JsonPropertyName("sig")]
        public partial string Sig { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("code")]
        public partial string ErrorCode { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("error")]
        public partial string ErrorMessage { get; set; } = string.Empty;
        #endregion
    }
}
