using static Android.Provider.Settings;

namespace AndreasReitberger.Shared.Core.Services
{
    public partial class DeviceProviderService
    {
        public static partial string? GetDeviceId()
            => Secure.GetString(Android.App.Application.Context?.ContentResolver, Secure.AndroidId);
    }
}
