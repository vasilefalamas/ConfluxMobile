using System;
using Conflux.Connectivity.Authentication;

namespace Conflux.Connectivity.GraphApi
{
    public class FacebookUriProvider
    {
        private const string AppId = "278580629014544";

        private const string ProductId = "df1610dbf5a740b9ac70d28cc2ff4b2b";

        public static Uri GetConnectionUri()
        {
            var defaultPermissions = string.Join(",", new[] { Permissions.PublicProfile, Permissions.ReadStream, Permissions.UserEvents, Permissions.UserLocation });

            return new Uri(string.Format(@"fbconnect://authorize?client_id={0}&scope={1}&redirect_uri=msft-{2}://authorize", AppId, defaultPermissions, ProductId));

            //    return new Uri(string.Format("https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&scope={2},{3},{4},{5}&display=popup&response_type=token",
            //                                Uri.EscapeDataString(AppId),
            //                                "msft-" + ProductId + "://authorize",
            //                                Permissions.PublicProfile,
            //                                Permissions.ReadStream,
            //                                Permissions.UserEvents,
            //                                Permissions.UserLocation));
        }

        public static Uri GetAppSelfRedirecturi()
        {
            return new Uri(string.Format("msft-{0}://authorize", ProductId));
        }

        public static Uri GetConnectionUri(params Permissions[] permissionList)
        {
            var permissions = string.Join(",", permissionList.ToString());

            return new Uri(string.Format(@"fbconnect://authorize?client_id={0}&scope={1}&redirect_uri=msft-{2}://authorize", AppId, permissions, ProductId));
        }

        public static Uri GetUserNameInfoUri(AccessToken accessToken)
        {
            return new Uri(string.Format("https://graph.facebook.com/me?access_token={0}", accessToken.Value));
        }

        public static Uri GetLocationIdUri(long locationId)
        {
            return new Uri(string.Format("https://graph.facebook.com/{0}", locationId));
        }

        public static Uri GetProfilePictureUri(AccessToken accessToken, PictureSize pictureSize = PictureSize.Size160x160)
        {
            return new Uri(string.Format("https://graph.facebook.com/me/picture?redirect=false&access_token={0}&width={1}&height={1}", accessToken.Value, (int)pictureSize));
        }

        public static Uri GetEventsByLocationKeywordUri(AccessToken accessToken, string locationKeyword, int offset, int? limit)
        {
            return new Uri(string.Format("https://graph.facebook.com/search?q={0}&type=event&limit={1}&offset={2}&access_token={3}", locationKeyword, limit, offset, accessToken.Value));
        }
    }
}
