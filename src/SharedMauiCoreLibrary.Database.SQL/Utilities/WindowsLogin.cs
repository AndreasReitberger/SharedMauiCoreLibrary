using System.Runtime.Versioning;
using System.Security.Principal;

namespace AndreasReitberger.Shared.Core.Database.Utilities
{
    [SupportedOSPlatform("windows")]
    public partial class WindowsLogin : IDisposable
    {
        protected const int LOGON32_PROVIDER_DEFAULT = 0;
        protected const int LOGON32_LOGON_INTERACTIVE = 2;

        public WindowsIdentity? Identity = null;
        private nint m_accessToken;


        [System.Runtime.InteropServices.LibraryImport("advapi32.dll", SetLastError = true, StringMarshalling = System.Runtime.InteropServices.StringMarshalling.Utf8)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static partial bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref nint phToken);

        [System.Runtime.InteropServices.LibraryImport("kernel32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static partial bool CloseHandle(nint handle);


        // AccessToken ==> this.Identity.AccessToken
        //public Microsoft.Win32.SafeHandles.SafeAccessTokenHandle AT
        //{
        //    get
        //    {
        //        var at = new Microsoft.Win32.SafeHandles.SafeAccessTokenHandle(this.m_accessToken);
        //        return at;
        //    }
        //}
        public WindowsLogin() => Identity = WindowsIdentity.GetCurrent();
        

        public WindowsLogin(string username, string domain, string password) => Login(username, domain, password);
        
        public void Login(string username, string domain, string password)
        {
            Identity?.Dispose();
            Identity = null;
            try
            {
                m_accessToken = new nint(0);
                Logout();

                m_accessToken = nint.Zero;
                bool logonSuccessfull = LogonUser(
                   username,
                   domain,
                   password,
                   LOGON32_LOGON_INTERACTIVE,
                   LOGON32_PROVIDER_DEFAULT,
                   ref m_accessToken);

                if (!logonSuccessfull)
                {
                    int error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                    throw new System.ComponentModel.Win32Exception(error);
                }
                Identity = new WindowsIdentity(m_accessToken);
            }
            catch
            {
                throw;
            }

        } // End Sub Login 

        public void Logout()
        {
            if (m_accessToken != nint.Zero)
                CloseHandle(m_accessToken);
            m_accessToken = nint.Zero;
            Identity?.Dispose();
            Identity = null;
        } // End Sub Logout 


        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Logout(); // End Sub Dispose 
            }
        }
    }
} // End Class WindowsLogin 
