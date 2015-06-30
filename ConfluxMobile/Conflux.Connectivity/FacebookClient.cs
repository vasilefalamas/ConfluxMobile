using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Xaml.Media.Imaging;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;
using Conflux.Connectivity.JsonExtensions;

namespace Conflux.Connectivity
{
    public class FacebookClient : IFacebookClient
    {
        private readonly IFacebookRequestHandler facebookRequestHandler;

        public FacebookClient(AccessToken accessToken)
        {
            facebookRequestHandler = new FacebookRequestHandler(accessToken);
        }

        public async Task<User> GetUserNameInfoAsync()
        {
            var response = await facebookRequestHandler.GetUserNameInfoAsync();

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
            var response = await facebookRequestHandler.GetLocationInfoAsync(locationId);

            JsonObject jsonObject = JsonValue.Parse(response).GetObject();

            return new LocationInfo
            {
                Id = Convert.ToInt64(jsonObject.GetNamedValue<string>("id")),
                Name = jsonObject.GetNamedValue<string>("name"),
                Latitude = Convert.ToInt64(jsonObject.GetNamedObject("location").GetNamedNumber("latitude")),
                Longitude = Convert.ToInt64(jsonObject.GetNamedObject("location").GetNamedNumber("longitude"))
            };
        }

        public async Task<BitmapImage> GetProfilePictureAsync(PictureSize pictureSize = PictureSize.Size160x160)
        {
            var response = await facebookRequestHandler.GetProfilePictureAsync(pictureSize);

            JsonObject jsonObject = JsonValue.Parse(response).GetObject().GetNamedObject("data");

            bool isPictureAvailable = !jsonObject.GetNamedBoolean("is_silhouette");

            if (isPictureAvailable)
            {
                var pictureUrl = jsonObject.GetNamedString("url");

                return new BitmapImage(new Uri(pictureUrl, UriKind.Absolute));
            }

            return null;
        }

        public async Task<IEnumerable<Event>> GetEventsByKeywordAsync(string locationKeyword, uint offset = 0, uint? limit = null, DateTime? since = null, DateTime? until = null)
        {
            var response = await facebookRequestHandler.GetEventsByKeywordAsync(locationKeyword, offset, limit, since, until);

            var events = await GetEventsFromResponse(response);
            return events;
        }

        public async Task<Event> GetEventAsync(string eventId)
        {
            var response = await facebookRequestHandler.GetEventAsync(eventId);
            
            JsonObject jsonObject = JsonValue.Parse(response).GetObject();

            var detailedEvent = new Event
            {
                Id = eventId,
                Title = jsonObject.GetNamedValue<string>("name"),
                Description = jsonObject.GetNamedValue<string>("description"),
                StartTime = jsonObject.GetNamedValue<DateTime?>("start_time"),
                EndTime = jsonObject.GetNamedValue<DateTime?>("end_time"),
                Location = GetEventLocationInfo(jsonObject)
            };

            return detailedEvent;
        }

        public async Task<IEnumerable<Event>> GetMyEventsAsync()
        {
            var response = await facebookRequestHandler.GetMyEvents();

            var events = await GetEventsFromResponse(response);
            return events;
        }
        
        public async Task<bool> PostEventAttendanceAsync(string eventId)
        {
            var response = await facebookRequestHandler.PostEventAttendance(eventId);

            return response.ToLower().Contains("error");
        }

        private async Task<IEnumerable<Event>> GetEventsFromResponse(string response)
        {
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
                        Location = new LocationInfo
                        {
                            Name = eventItem.GetNamedValue<string>("location")
                        }
                    });
                }

            });

            return events;
        }


        //TODO IMPORTANT : Review this method - it causes a bug which returns locations with null lat/long and ID = 0
        private LocationInfo GetEventLocationInfo(JsonObject eventJsonObject)
        {
            var locationName = eventJsonObject.GetNamedValue<string>("location");

            var venueJsonObject = eventJsonObject.GetNamedValue<JsonObject>("venue");

            if (venueJsonObject != null)
            {
                var longitude = venueJsonObject.GetNamedValue<double?>("longitude");
                var latitude = venueJsonObject.GetNamedValue<double?>("latitude");

                if (longitude == null || latitude == null)
                {
                    return new LocationInfo
                    {
                        Name = locationName
                    };
                }

                return new LocationInfo
                {
                    Id = Convert.ToInt64(venueJsonObject.GetNamedValue<string>("id") ?? "0"),
                    Latitude = latitude.Value,
                    Longitude = longitude.Value,
                    Name = locationName
                };
            }

            if (locationName != null)
            {
                return new LocationInfo
                {
                    Name = locationName
                };
            }

            return null;
        }

    }
}
