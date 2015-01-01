using System;
using System.Globalization;
using Windows.Storage;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;

namespace Conflux.Core.Settings
{
    public static class AppSettings
    {
        private const string AccessTokenKey = "Facebook_AccessToken_Value";
        
        private const string AccessTokenExpiryKey = "Facebook_AccessToken_Expiry";

        private const string LastKnownLocationNameKey = "Preferences_LastKnownLocation_Name";

        private const string LastKnownLongitudeKey = "Preferences_LastKnownLocation_Longitude";

        private const string LastKnownLatitudeKey = "Preferences_LastKnownLocation_Latitude";

        private const string UseLastKnownLocationKey = "Preferences_UseLastKnownLocation";

        private const string AllowImagesDownloadKey = "Preferences_AllowImagesDownload";

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

            var invariantExpiryDate = Convert.ToDateTime(tokenExpiry.ToString(), CultureInfo.InvariantCulture);
            
            return new AccessToken(tokenValue.ToString(), invariantExpiryDate);
        }

        public static void SetAccessToken(AccessToken newAccessToken)
        {
            SetValue(AccessTokenKey, newAccessToken.Value);

            var invariantExpiryDate = newAccessToken.Expiry.ToString(CultureInfo.InvariantCulture);

            SetValue(AccessTokenExpiryKey, invariantExpiryDate);
        }

        public static LocationInfo GetLastKnownLocationInfo()
        {
            return new LocationInfo
            {
                Name = (string) GetValue(LastKnownLocationNameKey),
                Longitude = (double) GetValue(LastKnownLongitudeKey),
                Latitude = (double) GetValue(LastKnownLatitudeKey)
            };
        }

        public static void SetLastKnownLocationInfo(LocationInfo locationInfo)
        {
            SetValue(LastKnownLocationNameKey, locationInfo.Name);
            SetValue(LastKnownLongitudeKey, locationInfo.Longitude);
            SetValue(LastKnownLatitudeKey, locationInfo.Latitude);
        }

        public static bool GetLastKnownLocationUsage()
        {
            object useLastKnownLocationValue = GetValue(UseLastKnownLocationKey);
            bool useLastKnownLocation =  useLastKnownLocationValue != null && (bool) useLastKnownLocationValue;

            return useLastKnownLocation;
        }

        public static void SetLastKnownLocationUsage(bool useLastKnownLocation)
        {
            SetValue(UseLastKnownLocationKey, useLastKnownLocation);
        }

        public static bool GetAllowImagesDownloadStatus()
        {
            object isImageDownloadAllowedValue = GetValue(AllowImagesDownloadKey);
            bool isImageDownloadAllowed = isImageDownloadAllowedValue != null && (bool)isImageDownloadAllowedValue;

            return isImageDownloadAllowed;
        }

        public static void SetImagesDownloadAllowedStatus(bool allowImageDownload)
        {
            SetValue(AllowImagesDownloadKey, allowImageDownload);
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
