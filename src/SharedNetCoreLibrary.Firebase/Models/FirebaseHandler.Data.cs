using AndreasReitberger.Shared.Core.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Database.Query;
using System.Collections;
using System.Collections.Concurrent;

namespace AndreasReitberger.Shared.Firebase
{
    public partial class FirebaseHandler : ObservableObject
    {
        #region Settings Sync
        public async Task<T?> GetDataAsync<T>(string child)
        {
            T? result = default;
            try
            {
                // Don't sync user data if logged out or for anonymous logins
                if (CurrentUser is null) return result;
                client ??= ConnectWithToken(Token);
                ChildQuery t = client
                    .Child(child)
                    ;
                string listJson = await t.OnceAsJsonAsync();
                result = JsonConvertHelper.ToObject<T>(listJson);
            }
            catch (Exception exc)
            {
                OnErrorEvent(new(exc));
            }
            return result;
        }
        #endregion

        #region DataOperations
        public async Task<bool> AddOrUpdateAppSettingsDictionaryAsync(Dictionary<string, Tuple<object, Type>> settings, string child = "settings")
        {
            try
            {
                // Don't sync user data if logged out or for anonymous logins
                if (CurrentUser is null || CurrentUser?.User?.IsAnonymous == true) return false;
                client ??= ConnectWithToken(Token);
                await client
                    .Child("users")
                    .Child(CurrentUser?.User.Uid)
                    .Child(child)
                    .PutAsync(settings);
                return true;
            }
            catch (Exception exc)
            {
                OnErrorEvent(new(exc));
            }
            return false;
        }
        public Task AddOrUpdateAppSettingsAsync(Tuple<string, Tuple<object, Type>> settings, string child = "settings")
            => AddOrUpdateAppSettingsAsync(settingsKey: settings.Item1, settings: settings.Item2, child: child);
        public async Task AddOrUpdateAppSettingsAsync(string settingsKey, Tuple<object, Type> settings, string child = "settings")
        {
            try
            {
                // Don't sync user data if logged out or for anonymous logins
                if (CurrentUser is null || CurrentUser.User?.IsAnonymous == true) return;
                bool update = false;
                CloudSettings = await GetAppSettingsConcurrentDictionaryAsync(child: child);
                if (CloudSettings.TryGetValue(settingsKey, out Tuple<object, Type>? value))
                {
                    if (!value.Equals(settings.Item2))
                    {
                        CloudSettings?.TryUpdate(settingsKey, settings, value);
                        update = true;
                    }
                }
                else
                {
                    CloudSettings?.TryAdd(settingsKey, settings);
                    update = true;
                }
                if (update && CloudSettings is not null)
                    // Must send always the whole dictionary, otherwise the other keys are deleted
                    await AddOrUpdateAppSettingsConcurrentDictionaryAsync(settings: CloudSettings, child: child);
            }
            catch (Exception exc)
            {
                OnErrorEvent(new(exc));
            }
        }
        public async Task AddOrUpdateAppSettingsConcurrentDictionaryAsync(ConcurrentDictionary<string, Tuple<object, Type>> settings, string child = "settings")
        {
            try
            {
                // Don't sync user data if logged out or for anonymous logins
                if (CurrentUser is null || CurrentUser?.User?.IsAnonymous == true) return;
                client ??= ConnectWithToken(Token);
                await client
                    .Child("users")
                    .Child(CurrentUser?.User.Uid)
                    .Child(child)
                    .PutAsync(settings);
            }
            catch (Exception exc)
            {
                OnErrorEvent(new(exc));
            }
        }
        public async Task AddOrUpdateUserDataAsync<T>(T data, string? child = null, bool appendData = false, string? key = null)
        {
            try
            {
                // Don't sync user data if logged out or for anonymous logins
                if (CurrentUser is null || CurrentUser?.User?.IsAnonymous == true) return;
                client ??= ConnectWithToken(Token);
                if (appendData && data is IList enumerable)
                {
                    T? currentData = await GetUserDataAsync<T>(child);
                    if (currentData is not null && currentData is IList list)
                    {
                        foreach (object? element in list)
                        {
                            if (!enumerable.Contains(element))
                                enumerable.Add(element);
                        }
                        ;
                    }
                }
                string encryptedJsonDate = string.Empty;
                if (EnableEncryption && key is not null)
                {
                    // Decrypt data before converting it
                    string? plainText = JsonConvertHelper.ToSettingsString(data);
                    if (plainText is not null)
                    {
                        encryptedJsonDate = EncryptionManager.EncryptStringToBase64String(plainText, key);
                        if (child is not null)
                            await client
                                .Child("users")
                                .Child(CurrentUser?.User.Uid)
                                .Child(child)
                                .PutAsync(encryptedJsonDate);
                        else
                            await client
                                .Child("users")
                                .Child(CurrentUser?.User.Uid)
                                .PutAsync(encryptedJsonDate);
                    }
                }
                else
                {
                    if (child is not null)
                        await client
                            .Child("users")
                            .Child(CurrentUser?.User.Uid)
                            .Child(child)
                            .PutAsync(data);
                    else
                        await client
                            .Child("users")
                            .Child(CurrentUser?.User.Uid)
                            .PutAsync(data);
                }
            }
            catch (Exception exc)
            {
                OnErrorEvent(new(exc));
            }
        }
        public async Task AddOrUpdateDefaultDataAsync<T>(T data, string child, bool appendData = false)
        {
            try
            {
                // Don't sync user data if logged out or for anonymous logins
                if (CurrentUser is null) return;
                client ??= ConnectWithToken(Token);
                if (appendData && data is IList enumerable)
                {
                    T? currentData = await GetUserDataAsync<T>(child);
                    if (currentData is not null && currentData is IList list)
                    {
                        foreach (object? element in list)
                        {
                            if (!enumerable.Contains(element))
                                enumerable.Add(element);
                        }
                        ;
                    }
                }
                await client
                    .Child(child)
                    .PutAsync(data);
            }
            catch (Exception exc)
            {
                OnErrorEvent(new(exc));
            }
        }
        public async Task<Dictionary<string, Tuple<object, Type>>> GetAppSettingsDictionaryAsync(string child = "settings")
        {
            Dictionary<string, Tuple<object, Type>> result = [];
            try
            {
                // Don't sync user data if logged out or for anonymous logins
                if (CurrentUser is null || CurrentUser?.User?.IsAnonymous == true) return result;
                client ??= ConnectWithToken(Token);
                ChildQuery t = client
                    .Child("users")
                    .Child(CurrentUser?.User.Uid)
                    .Child(child)
                    ;
                string listJson = await t.OnceAsJsonAsync();
                result = JsonConvertHelper.ToObject<Dictionary<string, Tuple<object, Type>>>(listJson) ?? [];
            }
            catch (Exception exc)
            {
                OnErrorEvent(new(exc));
            }
            return result;
        }
        public async Task<ConcurrentDictionary<string, Tuple<object, Type>>> GetAppSettingsConcurrentDictionaryAsync(string child = "settings")
        {
            ConcurrentDictionary<string, Tuple<object, Type>> result = [];
            try
            {
                // Don't sync user data if logged out or for anonymous logins
                if (CurrentUser is null || CurrentUser?.User?.IsAnonymous == true) return result;
                client ??= ConnectWithToken(Token);
                ChildQuery t = client
                    .Child("users")
                    .Child(CurrentUser?.User.Uid)
                    .Child(child)
                    ;
                string listJson = await t.OnceAsJsonAsync();
                result = JsonConvertHelper.ToObject<ConcurrentDictionary<string, Tuple<object, Type>>>(listJson) ?? [];
            }
            catch (Exception exc)
            {
                OnErrorEvent(new(exc));
            }
            return result;
        }
        public async Task<T?> GetUserDataAsync<T>(string? child = null, string userPath = "users", string? key = null)
        {
            T? result = default;
            try
            {
                // Don't sync user data if logged out or for anonymous logins
                if (CurrentUser is null || CurrentUser?.User?.IsAnonymous == true) return result;
                client ??= ConnectWithToken(Token);
                ChildQuery t = child is not null ? client
                     .Child(userPath)
                     .Child(CurrentUser?.User.Uid)
                     .Child(child)
                     : client
                     .Child(userPath)
                     .Child(CurrentUser?.User.Uid)
                     ;
                string listJson = await t.OnceAsJsonAsync();
                if (listJson == "null" || listJson is null)
                    listJson = string.Empty;
                if (EnableEncryption && key is not null)
                {
                    // Decrypt data before converting it
                    listJson = EncryptionManager.DecryptStringFromBase64String(listJson, key);
                }
                result = JsonConvertHelper.ToObject<T>(listJson);
            }
            catch (Exception exc)
            {
                OnErrorEvent(new(exc));
            }
            return result;
        }
        public async Task<T?> GetDefaultDataAsync<T>(string child)
        {
            T? result = default;
            try
            {
                // Don't sync user data if logged out
                if (CurrentUser is null) return result;
                client ??= ConnectWithToken(Token);
                ChildQuery t = client
                    .Child(child);
                string listJson = await t.OnceAsJsonAsync();
                result = JsonConvertHelper.ToObject<T>(listJson);
            }
            catch (Exception exc)
            {
                OnErrorEvent(new(exc));
            }
            return result;
        }
        #endregion

    }
}
