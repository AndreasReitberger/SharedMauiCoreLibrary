using System.Runtime.Versioning;
using System.Security.Principal;

namespace AndreasReitberger.Shared.Core.Database.Utilities
{
    public static class SqlDatabaseServiceHelper
    {

        [SupportedOSPlatform("windows")]
        public static async Task ImpersonateAsync(Action action, string user, string userdomain, string password)
        {
            using WindowsLogin wi = new(user, userdomain, password);
            if (wi.Identity?.AccessToken is not null)
            {
                await WindowsIdentity.RunImpersonated(wi.Identity.AccessToken, async () =>
                {
                    WindowsIdentity useri = WindowsIdentity.GetCurrent();
                    action.Invoke();
                });
            }
        }
    }
}
