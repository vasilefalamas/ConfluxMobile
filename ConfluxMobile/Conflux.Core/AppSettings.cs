using System;
using Windows.Storage;
using Conflux.Connectivity.Authentication;

namespace Conflux.Core
{
    public static class AppSettings
    {
        public static AccessToken GetAccessToken()
        {
            var tokenValue = GetValue("AccessTokenValue");
            var tokenExpiry = GetValue("AccessTokenExpiry");

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
            SetValue("AccessTokenValue", newAccessToken.Value);
            SetValue("AccessTokenExpiry", newAccessToken.Expiry.ToString());
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
