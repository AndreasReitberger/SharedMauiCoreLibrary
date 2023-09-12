using AndreasReitberger.Shared.Core.Licensing.Enums;
using AndreasReitberger.Shared.Core.Licensing.Envato;
using AndreasReitberger.Shared.Core.Licensing.Events;
using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using AndreasReitberger.Shared.Core.Licensing.WooCommerce;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Text.RegularExpressions;

namespace AndreasReitberger.Shared.Core.Licensing
{
    public partial class LicenseManager : ObservableObject, ILicenseManager
    {
        #region Variables
        RestClient client;
        #endregion

        #region Properties

        [ObservableProperty]
        Uri licenseServer;

        [ObservableProperty]
        int? port = null;

#nullable enable
        [ObservableProperty]
        string? accessToken;

        [ObservableProperty]
        ILicenseInfo? currentLicense;
#nullable disable
        #endregion

        #region Ctor
        public LicenseManager() { }
        public LicenseManager(Uri licenseServer)
        {
            LicenseServer = licenseServer;
        }

        #endregion

        #region Methods

        public void Initialize(Uri licenseServer = null, int? port = null)
        {
            if (licenseServer != null) LicenseServer = licenseServer;
            if (port != null)
            {
                Port = port;
                client = new RestClient($"{LicenseServer}:{Port}");
            }
            else
                client = new(LicenseServer);
        }

        public async Task<ILicenseQueryResult> ActivateLicenseAsync(ILicenseInfo license, LicenseServerTarget target, Func<string> OnSuccess = null, Func<string> OnError = null)
        {
            if (client == null) Initialize();
            LicenseQueryResult result = new() { Success = false, TimeStamp = DateTimeOffset.Now };
            if (license == null) return result;
            if (license?.Options?.VerifyLicenseFormat == true && !string.IsNullOrEmpty(license?.Options?.LicenseCheckPattern))
            {
                bool licenseFormatValid = VerifyLicenseFormat(license, license?.Options.LicenseCheckPattern);
                result.Message = "License format is invalid";
                return result;
            }
            switch (target)
            {
                case LicenseServerTarget.WooCommerce:
                    WooActivationResponse[] wooResult = await QueryWooCommerceAsync(WooSoftwareLicenseAction.Activate, license).ConfigureAwait(false);
                    if (wooResult?.All(result => result.Status == "success" && (result.ErrorCode == "s100" || result.ErrorCode == "s101")) == true)
                    {
                        result = new()
                        {
                            Success = true,
                            Valid = true,
                            Message = string.Join("|", wooResult?.Select(result => result.ErrorMessage)),
                            TimeStamp = DateTimeOffset.Now,
                        };
                    }
                    else
                    {
                        result = new()
                        {
                            Success = false,
                            Valid = false,
                            Message = string.Join("|", wooResult?.Select(result => result.ErrorMessage)),
                            TimeStamp = DateTimeOffset.Now,
                        };
                    }
                    break;
                case LicenseServerTarget.Envato:
                    throw new NotSupportedException($"The {target} doesn't support this function!");
                case LicenseServerTarget.Custom:
                default:
                    throw new NotImplementedException($"The features for {target} aren' implemented yet!");
                    //break;
            }
            if (result.Success) OnSuccess?.Invoke();
            else OnError?.Invoke();

            // Update the license
            license.IsValid = result.Valid;
            license.IsActive = result.Valid;

            OnLicenseChanged(new LicenseChangedEventArgs()
            {
                Message = result.Message,
                CheckDate = result.TimeStamp,
                Valid = result.Valid,
                LicenseKey = license.License,
            });
            return result;
        }

        public async Task<ILicenseQueryResult> CheckLicenseAsync(ILicenseInfo license, LicenseServerTarget target, Func<string> OnSuccess = null, Func<string> OnError = null)
        {
            if (client == null) Initialize();
            LicenseQueryResult result = new() { Success = false, Valid = false, TimeStamp = DateTimeOffset.Now };
            if (license == null) return result;
            if (license?.Options?.VerifyLicenseFormat == true && !string.IsNullOrEmpty(license?.Options?.LicenseCheckPattern))
            {
                bool licenseFormatValid = VerifyLicenseFormat(license, license?.Options.LicenseCheckPattern);
                result.Message = "License format is invalid";
                return result;
            }
            switch (target)
            {
                case LicenseServerTarget.WooCommerce:
                    WooActivationResponse[] wooResult = await QueryWooCommerceAsync(WooSoftwareLicenseAction.StatusCheck, license).ConfigureAwait(false);
                    if (wooResult?.All(result => result.Status == "success" && (result.ErrorCode == "s205" || result.ErrorCode == "s215")) == true)
                    {
                        result = new()
                        {
                            Success = true,
                            Valid = true,
                            Message = string.Join("|", wooResult?.Select(result => result.ErrorMessage)),
                            TimeStamp = DateTimeOffset.Now,
                        };
                    }
                    else
                    {
                        result = new()
                        {
                            Success = false,
                            Valid = false,
                            Message = string.Join("|", wooResult?.Select(result => result.ErrorMessage)),
                            TimeStamp = DateTimeOffset.Now,
                        };
                    }
                    break;
                case LicenseServerTarget.Envato:
                    EnvatoVerifyPurchaseCodeRespone envatoResult = await QueryEnvatoAsync(license).ConfigureAwait(false);
                    if (envatoResult?.Item?.Id == license?.ProductCode)
                    {
                        result = new()
                        {
                            Success = true,
                            Valid = true,
                            Message = "",
                            TimeStamp = DateTimeOffset.Now,
                        };
                    }
                    else
                    {
                        result = new()
                        {
                            Success = false,
                            Valid = false,
                            Message = envatoResult == null ? "Result was null" : $"The ItemId's don't match! Got: {envatoResult?.Item?.Id} Expected: {license?.ProductCode}",
                            TimeStamp = DateTimeOffset.Now,
                        };
                    }
                    break;
                case LicenseServerTarget.Custom:
                default:
                    throw new NotImplementedException($"The features for {target} aren' implemented yet!");
                    //break;
            }
            if (result.Success) OnSuccess?.Invoke();
            else OnError?.Invoke();

            // Update the license
            license.IsValid = result.Valid;
            license.IsActive = result.Valid;

            OnLicenseChanged(new LicenseChangedEventArgs()
            {
                Message = result.Message,
                CheckDate = result.TimeStamp,
                Valid = result.Valid,
                LicenseKey = license.License,
            });
            return result;
        }

        public async Task<ILicenseQueryResult> DeactivateLicenseAsync(ILicenseInfo license, LicenseServerTarget target, Func<string> OnSuccess = null, Func<string> OnError = null)
        {
            if (client == null) Initialize();
            LicenseQueryResult result = new() { Success = false, Valid = false, TimeStamp = DateTimeOffset.Now };
            if (license == null) return result;
            if (license?.Options?.VerifyLicenseFormat == true && !string.IsNullOrEmpty(license?.Options?.LicenseCheckPattern))
            {
                bool licenseFormatValid = VerifyLicenseFormat(license, license?.Options.LicenseCheckPattern);
                result.Message = "License format is invalid";
                return result;
            }
            switch (target)
            {
                case LicenseServerTarget.WooCommerce:
                    WooActivationResponse[] wooResult = await QueryWooCommerceAsync(WooSoftwareLicenseAction.Deactivate, license).ConfigureAwait(false);
                    if (wooResult?.All(result => result.Status == "success" && (result.ErrorCode == "s201")) == true)
                    {
                        result = new()
                        {
                            Success = true,
                            Valid = false,
                            Message = string.Join("|", wooResult?.Select(result => result.ErrorMessage)),
                            TimeStamp = DateTimeOffset.Now,
                        };
                    }
                    else
                    {
                        result = new()
                        {
                            Success = false,
                            Valid = false,
                            Message = string.Join("|", wooResult?.Select(result => result.ErrorMessage)),
                            TimeStamp = DateTimeOffset.Now,
                        };
                    }
                    break;
                case LicenseServerTarget.Envato:
                    throw new NotSupportedException($"The {target} doesn't support this function!");
                case LicenseServerTarget.Custom:
                default:
                    throw new NotImplementedException($"The features for {target} aren' implemented yet!");
                    //break;
            }
            if (result.Success)
            { 
                OnSuccess?.Invoke();
                CurrentLicense = null;
            }
            else OnError?.Invoke();

            // Update the license
            license.IsValid = result.Valid;
            license.IsActive = result.Valid;

            OnLicenseChanged(new LicenseChangedEventArgs()
            {
                Message = result.Message,
                CheckDate = result.TimeStamp,
                Valid = false,
                LicenseKey = license.License,
            });
            return result;
        }

        public async Task<ILicenseQueryResult> DeleteLicenseAsync(ILicenseInfo license, LicenseServerTarget target, Func<string> OnSuccess = null, Func<string> OnError = null)
        {
            if (client == null) Initialize();
            LicenseQueryResult result = new() { Success = false, Valid = false, TimeStamp = DateTimeOffset.Now };
            if (license == null) return result;
            if (license?.Options?.VerifyLicenseFormat == true && !string.IsNullOrEmpty(license?.Options?.LicenseCheckPattern))
            {
                bool licenseFormatValid = VerifyLicenseFormat(license, license?.Options.LicenseCheckPattern);
                result.Message = "License format is invalid";
                return result;
            }
            switch (target)
            {
                case LicenseServerTarget.WooCommerce:
                    WooActivationResponse[] wooResult = await QueryWooCommerceAsync(WooSoftwareLicenseAction.DeleteKey, license).ConfigureAwait(false);
                    if (wooResult?.All(result => result.Status == "success" && (result.ErrorCode == "s205")) == true)
                    {
                        result = new()
                        {
                            Success = true,
                            Valid = false,
                            Message = string.Join("|", wooResult?.Select(result => result.ErrorMessage)),
                            TimeStamp = DateTimeOffset.Now,
                        };
                    }
                    else
                    {
                        result = new()
                        {
                            Success = false,
                            Valid = false,
                            Message = string.Join("|", wooResult?.Select(result => result.ErrorMessage)),
                            TimeStamp = DateTimeOffset.Now,
                        };
                    }
                    break;
                case LicenseServerTarget.Envato:
                    throw new NotSupportedException($"The {target} doesn't support this function!");
                case LicenseServerTarget.Custom:
                default:
                    throw new NotImplementedException($"The features for {target} aren' implemented yet!");
                    //break;
            }
            if (result.Success)
            {
                OnSuccess?.Invoke();
                CurrentLicense = null;
            }
            else OnError?.Invoke();

            // Update the license
            license.IsValid = result.Valid;
            license.IsActive = result.Valid;

            OnLicenseChanged(new LicenseChangedEventArgs()
            {
                Message = result.Message,
                CheckDate = result.TimeStamp,
                Valid = false,
                LicenseKey = license.License,
            });
            return result;
        }

        public async Task<IApplicationVersionResult> GetLatestApplicationVersionAsync(ILicenseInfo license, LicenseServerTarget target, Func<string> OnSuccess = null, Func<string> OnError = null)
        {
            if (client == null) Initialize();
            ApplicationVersionResult result = new() { Success = false, TimeStamp = DateTimeOffset.Now };
            if (license == null) return result;
            switch (target)
            {
                case LicenseServerTarget.WooCommerce:
                    WooCodeVersionResponse[] wooResult = await QueryLatestApplicationVersionFromWooCommerceAsync(WooSoftwareLicenseAction.CodeVersion, license).ConfigureAwait(false);
                    if (wooResult?.All(result => result.Status == "success" && (result.ErrorCode == "s403")) == true)
                    {
                        result = new()
                        {
                            Success = true,
                            //Message = string.Join("|", wooResult?.Select(result => result.VersionMessage)),
                            TimeStamp = DateTimeOffset.Now,
                        };
                    }
                    else
                    {
                        result = new()
                        {
                            Success = false,
                            //Message = string.Join("|", wooResult?.Select(result => result.VersionMessage)),
                            TimeStamp = DateTimeOffset.Now,
                        };
                    }
                    break;
                case LicenseServerTarget.Envato:
                    throw new NotSupportedException($"The {target} doesn't support this function!");
                case LicenseServerTarget.Custom:
                default:
                    throw new NotImplementedException($"The features for {target} aren' implemented yet!");
                    //break;
            }
            if (result.Success) OnSuccess?.Invoke();
            else OnError?.Invoke();
            return result;
        }

        public bool VerifyLicenseFormat(ILicenseInfo license, string checkPattern)
        {
            if (string.IsNullOrEmpty(license?.License)) return false;
            Regex check = new(checkPattern);
            return check.IsMatch(license?.License);
        }

        #region Private
        async Task<string> RestApiCallAsync(
        string command,
        Method method = Method.Get,
        Dictionary<string, string> parameters = null,
        Dictionary<string, string> headers = null,
        CancellationTokenSource cts = default
        )
        {
            string result = string.Empty;
            if (cts == default)
            {
                cts = new(10000);
            }

            RestRequest request = new(command, method)
            {
                RequestFormat = DataFormat.Json
            };
            if (headers?.Count > 0)
            {
                foreach (KeyValuePair<string, string> pair in headers)
                {
                    request.AddHeader(pair.Key, pair.Value);
                }
            }
            if (parameters?.Count > 0)
            {
                foreach (KeyValuePair<string, string> pair in parameters)
                {
                    request.AddQueryParameter(pair.Key, pair.Value);
                }
            }

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Uri fullUri = client.BuildUri(request);
            RestResponse respone = await client.ExecuteAsync(request, cts.Token).ConfigureAwait(false);
            if (respone.StatusCode == HttpStatusCode.OK && respone.ResponseStatus == ResponseStatus.Completed)
            {
                result = respone?.Content;
            }
            return result;
        }

        #region WooCommerce
        async Task<WooActivationResponse[]> QueryWooCommerceAsync(string action, ILicenseInfo license)
        {
            try
            {
                string command = string.Empty;

                Dictionary<string, string> parameters = new() {
                    { "woo_sl_action", action },
                    { "product_unique_id", license.ProductCode },
                    { "licence_key", license.License },
                };
                if (action != WooSoftwareLicenseAction.DeleteKey)
                {
                    parameters.Add("domain", license.Domain);
                }
                string jsonResult = await RestApiCallAsync(command, Method.Get, parameters, new(10000));

                WooActivationResponse[] result = JsonConvert.DeserializeObject<WooActivationResponse[]>(jsonResult);
                return result;
            }
            catch (Exception exc)
            {
                OnError(new ErrorEventArgs(exc) { });
                return null;
            }
        }
        async Task<WooCodeVersionResponse[]> QueryLatestApplicationVersionFromWooCommerceAsync(string action, ILicenseInfo license)
        {
            try
            {
                string command = string.Empty;
                Dictionary<string, string> parameters = new() {
                    { "woo_sl_action", action },
                    { "product_unique_id", license.ProductCode },
                };
                if (license?.License != null)
                {
                    parameters.Add(
                    "licence_key", license.License);
                    if (action != WooSoftwareLicenseAction.DeleteKey)
                    {
                        parameters.Add("domain", license.Domain);
                    }
                }
                string jsonResult = await RestApiCallAsync(command, Method.Get, parameters, new(10000));

                WooCodeVersionResponse[] result = JsonConvert.DeserializeObject<WooCodeVersionResponse[]>(jsonResult);
                return result;
            }
            catch (Exception exc)
            {
                OnError(new ErrorEventArgs(exc) { });
                return null;
            }
        }
        #endregion

        #region Envato
        async Task<EnvatoVerifyPurchaseCodeRespone> QueryEnvatoAsync(ILicenseInfo license)
        {
            try
            {
                string command = string.Empty;
                if (string.IsNullOrEmpty(AccessToken))
                    throw new InvalidDataException($"To query data from `Envato`, an `AccessToken` is mandatory. However this token was null or empty!");
                Dictionary<string, string> headers = new() {
                    { "Authorization", $"Bearer {AccessToken}" },
                };
                Dictionary<string, string> parameters = new() {
                    { "code", license.License },
                };
                string jsonResult = await RestApiCallAsync(command, Method.Get, parameters: parameters, headers: headers, new(10000));

                EnvatoVerifyPurchaseCodeRespone result = JsonConvert.DeserializeObject<EnvatoVerifyPurchaseCodeRespone>(jsonResult);
                return result;
            }
            catch (Exception exc)
            {
                OnError(new ErrorEventArgs(exc) { });
                return null;
            }
        }
        #endregion

        #endregion

        #endregion
    }
}
