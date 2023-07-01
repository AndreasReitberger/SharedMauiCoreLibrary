using static Android.Provider.Settings;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class DeviceProviderService
    {
        public partial string GetDeviceId()
        {
            return Secure.GetString(Android.App.Application.Context.ContentResolver, Secure.AndroidId);
        }
    }
}
