using System;

namespace Conflux.Connectivity.Authentication
{
    public class AccessToken
    {
        public string Value { get; private set; }

        public DateTime Expiry { get; private set; }

        public AccessToken(string token)
        {
            Value = token;
        }

        public AccessToken(string token, DateTime expiry)
        {
            Value = token;
            Expiry = expiry;
        }

        public static AccessToken Create(string authenticationResponseData)
        {
            int index = authenticationResponseData.IndexOf("access_token", StringComparison.Ordinal);

            var responseData = authenticationResponseData.Substring(index);
            var keyValPairs = responseData.Split('&');

            string accessToken = null;
            string expiresIn = null;

            foreach (string item in keyValPairs)
            {
                var splits = item.Split('=');
                switch (splits[0])
                {
                    case "access_token":
                        accessToken = splits[1];
                        break;
                    case "expires_in":
                        expiresIn = splits[1];
                        break;
                }
            }

            var expiryDate = DateTime.Now.AddSeconds(Convert.ToDouble(expiresIn));

            return new AccessToken(accessToken, expiryDate);
        }
    }
}
