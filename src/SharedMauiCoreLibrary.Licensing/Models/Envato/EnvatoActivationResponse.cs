﻿using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

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
        [JsonProperty("activated")]
        bool activated = false;

        [ObservableProperty]
        [JsonProperty("instance")]
        int instance;

        [ObservableProperty]
        [JsonProperty("message")]
        string message = string.Empty;

        [ObservableProperty]
        [JsonProperty("timestamp")]
        int timestamp;

        [ObservableProperty]
        [JsonProperty("sig")]
        string sig = string.Empty;

        [ObservableProperty]
        [JsonProperty("code")]
        string errorCode = string.Empty;

        [ObservableProperty]
        [JsonProperty("error")]
        string errorMessage = string.Empty;
        #endregion
    }
}
