using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AndreasReitberger.Shared.Core.REST
{
#if RestClient
    [Obsolete("Move to own extension library")]
    public partial class RestClientBase
    {
    #region Variable
        //RestClient restClient;
        HttpClient httpClient;

    #endregion

    #region Properties
        const string _appBaseUrl = "https://api.lexoffice.io/";
        const string _apiVersion = "v1";
    #endregion

    #region Methods

    #region Proxy
        Uri GetProxyUri()
        {
            return ProxyAddress.StartsWith("http://") || ProxyAddress.StartsWith("https://") ? new Uri($"{ProxyAddress}:{ProxyPort}") : new Uri($"{(SecureProxyConnection ? "https" : "http")}://{ProxyAddress}:{ProxyPort}");
        }

        WebProxy GetCurrentProxy()
        {
            WebProxy proxy = new()
            {
                Address = GetProxyUri(),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = ProxyUseDefaultCredentials,
            };
            if (ProxyUseDefaultCredentials && !string.IsNullOrEmpty(ProxyUser))
            {
                proxy.Credentials = new NetworkCredential(ProxyUser, ProxyPassword);
            }
            else
            {
                proxy.UseDefaultCredentials = ProxyUseDefaultCredentials;
            }
            return proxy;
        }
    #endregion

    #region OnlineCheck
        public async Task CheckOnlineAsync(int Timeout = 10000)
        {
            if (IsConnecting) return; // Avoid multiple calls
            IsConnecting = true;
            bool isReachable = false;
            try
            {
                // Cancel after timeout
                var cts = new CancellationTokenSource(new TimeSpan(0, 0, 0, 0, Timeout));
                string uriString = $"{_appBaseUrl}";

                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(uriString, cts.Token).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    if (response != null)
                    {
                        isReachable = response.IsSuccessStatusCode;
                    }
                }
                catch (InvalidOperationException iexc)
                {
                    OnError(new UnhandledExceptionEventArgs(iexc, false));
                }
                catch (HttpRequestException rexc)
                {
                    OnError(new UnhandledExceptionEventArgs(rexc, false));
                }
                catch (TaskCanceledException)
                {
                    // Throws exception on timeout, not actually an error
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            IsConnecting = false;
            IsOnline = isReachable;
            //return isReachable;
        }
    #endregion

    #endregion
    }
#endif
}
