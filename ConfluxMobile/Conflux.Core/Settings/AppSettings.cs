using System;
using Windows.Storage;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;

namespace Conflux.Core.Settings
{
    public static class AppSettings
    {
        private const string AccessTokenKey = "AccessTokenValue";
        
        private const string AccessTokenExpiryKey = "AccessTokenExpiry";

        private const string CachedCityKey = "CachedCity";

        private const string CachedLongitudeKey = "CachedLongitude";

        private const string CachedLatitudeKey = "CachedLatitude";

        private const string IsLocationCachedKey = "IsLocationCached";

        private const string IsImageDownloadAllowedKey = "IsImageDownloadAllowed";

        public static AccessToken GetAccessToken()
        {
            var tokenValue = GetValue(AccessTokenKey);
            var tokenExpiry = GetValue(AccessTokenExpiryKey);

            if (tokenValue == null)
            {
                return null;
            }

            if (tokenExpiry == null)
            {
                return new AccessToken(tokenValue.ToString());
            }

            return new AccessToken(tokenValue.ToString(), Convert.ToDateTime(tokenExpiry));
        }

        public static void SetAccessToken(AccessToken newAccessToken)
        {
            SetValue(AccessTokenKey, newAccessToken.Value);
            SetValue(AccessTokenExpiryKey, newAccessToken.Expiry.ToString());
        }

        public static LocationInfo GetCachedLocationInfo()
        {
            return new LocationInfo
            {
                Name = (string) GetValue(CachedCityKey),
                Longitude = (double) GetValue(CachedLongitudeKey),
                Latitude = (double) GetValue(CachedLatitudeKey)
            };
        }

        public static void SetCachedLocationInfo(LocationInfo locationInfo)
        {
            SetValue(CachedCityKey, locationInfo.Name);
            SetValue(CachedLongitudeKey, locationInfo.Longitude);
            SetValue(CachedLatitudeKey, locationInfo.Latitude);
        }

        public static bool GetLocationCacheStatus()
        {
            object isLocationCachedRawValue = GetValue(IsLocationCachedKey);
            bool isLocationCached =  isLocationCachedRawValue != null && (bool) isLocationCachedRawValue;

            return isLocationCached;
        }

        public static void SetLocationCacheStatus(bool isLocationCached)
        {
            SetValue(IsLocationCachedKey, isLocationCached);
        }

        public static bool GetAllowImageDownloadStatus()
        {
            object isImageDownloadAllowedRawValue = GetValue(IsImageDownloadAllowedKey);
            bool isImageDownloadAllowed = isImageDownloadAllowedRawValue != null && (bool)isImageDownloadAllowedRawValue;

            return isImageDownloadAllowed;
        }

        public static void SetImageDownloadAllowedStatus(bool allowImageDownload)
        {
            SetValue(IsImageDownloadAllowedKey, allowImageDownload);
        }

        private static object GetValue(string key)
        {
            var settings = ApplicationData.Current.LocalSettings;

            return settings.Values.ContainsKey(key) ? settings.Values[key] : null;
        }

        private static void SetValue(string key, object settingsObject)
        {
            var settings = ApplicationData.Current.LocalSettings;

            settings.Values[key] = settingsObject;
        }
    }
}
