using System;
using System.Text;
using Conflux.Connectivity.Authentication;

namespace Conflux.Connectivity.GraphApi
{
    public class FacebookUriCollection
    {
        private const string AppId = "278580629014544";

        private const string ProductId = "df1610dbf5a740b9ac70d28cc2ff4b2b";

        public static Uri GetConnectionUri()
        {
            var defaultPermissions = string.Join(",", new[] { Permissions.PublicProfile, Permissions.ReadStream, Permissions.UserEvents, Permissions.RsvpEvent, Permissions.UserLocation });

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

        public static Uri GetEventsByLocationKeywordUri(AccessToken accessToken, string locationKeyword, uint offset, uint? limit, DateTime? since = null, DateTime? until = null)
        {
            var uriString = new StringBuilder(string.Format("https://graph.facebook.com/search?q={0}&type=event&limit={1}&offset={2}&access_token={3}", locationKeyword, limit, offset, accessToken.Value));

            if (since != null)
            {
                var startDate = since.Value;
                uriString.Append(string.Format("&since={0}-{1}-{2}", startDate.Year, startDate.Month, startDate.Day));
            }

            if (until != null)
            {
                var endDate = until.Value;
                uriString.Append(string.Format("&until={0}-{1}-{2}", endDate.Year, endDate.Month, endDate.Day));
            }

            return new Uri(uriString.ToString());
        }

        public static Uri GetEventDetailsUri(AccessToken accessToken, string eventId)
        {
            return new Uri(string.Format("https://graph.facebook.com/{0}?access_token={1}", eventId, accessToken.Value));
        }

        public static Uri GetMyEventsUri(AccessToken accessToken)
        {
            return new Uri(string.Format("https://graph.facebook.com/me/events?access_token={0}", accessToken.Value));
        }

        public static Uri GetEventAttendanceUri(AccessToken accessToken, string eventId)
        {
            return new Uri(string.Format("https://graph.facebook.com/{0}/attending?access_token={1}", eventId, accessToken.Value));
        }
    }
}
