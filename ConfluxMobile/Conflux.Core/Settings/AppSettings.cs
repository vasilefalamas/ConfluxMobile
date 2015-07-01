using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Windows.Storage;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;

namespace Conflux.Core.Settings
{
    public static class AppSettings
    {
        private const string AccessTokenKey = "Facebook_AccessToken_Value";
        
        private const string AccessTokenExpiryKey = "Facebook_AccessToken_Expiry";

        private const string LastKnownLocationNameKey = "Settings_LastKnownLocation_Name";

        private const string LastKnownLongitudeKey = "Settings_LastKnownLocation_Longitude";

        private const string LastKnownLatitudeKey = "Settings_LastKnownLocation_Latitude";

        private const string UseLastKnownLocationKey = "Settings_UseLastKnownLocation";

        private const string AllowImagesDownloadKey = "Settings_AllowImagesDownload";

        private const string TermsOfUseAcceptedKey = "Settings_TermsOfUseAccepted";

        private const string BlacklistEventsKey = "Storage_BlackListEvents";

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

        public static bool GetTermsOfUseAcceptStatus()
        {
            object hasTermsOfUseAccepted = GetValue(TermsOfUseAcceptedKey);
            bool result = hasTermsOfUseAccepted != null && (bool) hasTermsOfUseAccepted;

            return result;
        }

        public static void SetTermsOfUseAcceptStatus(bool isAccepted)
        {
            SetValue(TermsOfUseAcceptedKey, isAccepted);
        }

        public static List<string> GetBlacklistEventsIds()
        {
            var rawString = (String) GetValue(BlacklistEventsKey) ?? string.Empty; 

            var resultList = rawString.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            return resultList.ToList();
        }

        public static void AddBlacklistEvent(string eventId)
        {
            var rawString = (String)GetValue(BlacklistEventsKey) ?? string.Empty;

            var joinedString = string.Format("{0},{1}", rawString, eventId);
            SetValue(BlacklistEventsKey, joinedString);
        }

        public static void RemoveBlacklistEvent(string eventId)
        {
            var rawString = (String)GetValue(BlacklistEventsKey) ?? string.Empty;

            SetValue(BlacklistEventsKey, rawString.Replace(eventId, string.Empty));
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
