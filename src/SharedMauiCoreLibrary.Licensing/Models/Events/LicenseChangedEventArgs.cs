using Newtonsoft.Json;

namespace AndreasReitberger.Shared.Core.Licensing.Events
{
    public class LicenseChangedEventArgs : EventArgs
    {
        #region Properties
        public string Message { get; set; }
        public DateTimeOffset CheckDate { get; set; }
        public string LicenseKey { get; set; }
        public bool Valid { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}
