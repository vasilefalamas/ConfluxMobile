using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Xaml.Media.Imaging;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;
using Conflux.Connectivity.JsonExtensions;

namespace Conflux.Connectivity
{
    public class FacebookProvider : IFacebookProvider
    {
        private readonly HttpClient httpClient;

        public FacebookProvider()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Gets facebook user basic data based on the provided access token.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<User> GetUserNameInfoAsync(AccessToken accessToken)
        {
            var response = await httpClient.GetStringAsync(FacebookUriProvider.GetUserNameInfoUri(accessToken));

            JsonObject jsonObject = JsonValue.Parse(response).GetObject();

            return new User
            {
                Id = Convert.ToInt64(jsonObject.GetNamedValue<string>("id")),
                FirstName = jsonObject.GetNamedValue<string>("first_name"),
                MiddleName = jsonObject.GetNamedValue<string>("middle_name"),
                LastName = jsonObject.GetNamedValue<string>("last_name"),
                FullName = jsonObject.GetNamedValue<string>("name")
            };
        }

        public async Task<LocationInfo> GetLocationInfoAsync(long locationId)
        {
            var response = await httpClient.GetStringAsync(FacebookUriProvider.GetLocationIdUri(locationId));

            JsonObject jsonObject = JsonValue.Parse(response).GetObject();

            return new LocationInfo
            {
                Id = Convert.ToInt64(jsonObject.GetNamedValue<string>("id")),
                Name = jsonObject.GetNamedValue<string>("name"),
                Latitude = Convert.ToInt64(jsonObject.GetNamedObject("location").GetNamedNumber("latitude")),
                Longitude = Convert.ToInt64(jsonObject.GetNamedObject("location").GetNamedNumber("longitude"))
            };
        }

        public AuthenticationResult GetAuthenticationResult(string authenticationResponse)
        {
            if (authenticationResponse.Contains("error") || authenticationResponse.Contains("error_code"))
            {
                return new AuthenticationResult(AuthenticationState.Failed, null);
            }

            var accessToken = AccessToken.Create(authenticationResponse);

            return new AuthenticationResult(AuthenticationState.Success, accessToken);
        }


        public async Task<BitmapImage> GetProfilePictureAsync(AccessToken accessToken, PictureSize pictureSize = PictureSize.Size160x160)
        {
            var response = await httpClient.GetStringAsync(FacebookUriProvider.GetProfilePictureUri(accessToken, pictureSize));

            JsonObject jsonObject = JsonValue.Parse(response).GetObject().GetNamedObject("data");

            bool isPictureAvailable = !jsonObject.GetNamedBoolean("is_silhouette");

            if (isPictureAvailable)
            {
                var pictureUrl = jsonObject.GetNamedString("url");

                return new BitmapImage(new Uri(pictureUrl, UriKind.Absolute));
            }

            return null;
        }

        public async Task<IEnumerable<Event>> GetEventsByKeywordAsync(AccessToken accessToken, string locationKeyword, uint offset = 0, uint? limit = null)
        {
            var response = await httpClient.GetStringAsync(FacebookUriProvider.GetEventsByLocationKeywordUri(accessToken, locationKeyword, offset, limit));

            var events = new List<Event>();

            await Task.Factory.StartNew(() =>
            {
                JsonObject jsonObject = JsonValue.Parse(response).GetObject();

                var eventsArray = jsonObject.GetNamedArray("data");

                foreach (var eventObject in eventsArray)
                {
                    var eventItem = eventObject.GetObject();

                    events.Add(new Event
                    {
                        Id = eventItem.GetNamedValue<string>("id"),
                        Title = eventItem.GetNamedValue<string>("name"),
                        StartTime = eventItem.GetNamedValue<DateTime?>("start_time"),
                        EndTime = eventItem.GetNamedValue<DateTime?>("end_time"),
                        Location = eventItem.GetNamedValue<string>("location")
                    });
                }

            });

            return events;
        }
    }
}
