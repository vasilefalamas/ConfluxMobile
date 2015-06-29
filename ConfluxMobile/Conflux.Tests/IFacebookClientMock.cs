using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Xaml.Media.Imaging;
using Conflux.Connectivity.GraphApi;
using Conflux.Connectivity.JsonExtensions;

namespace Conflux.Tests
{
    public class FacebookClientMock : IFacebookClient
    {
        private readonly IFacebookRequestHandler requestHandlerMock = new FacebookRequestHandlerMock();

        public async Task<User> GetUserNameInfoAsync()
        {
            var result = await requestHandlerMock.GetUserNameInfoAsync();

            JsonObject jsonObject = JsonValue.Parse(result).GetObject();

            return new User
            {
                Id = Convert.ToInt64(jsonObject.GetNamedValue<string>("id")),
                FirstName = jsonObject.GetNamedValue<string>("first_name"),
                MiddleName = jsonObject.GetNamedValue<string>("middle_name"),
                LastName = jsonObject.GetNamedValue<string>("last_name"),
                FullName = jsonObject.GetNamedValue<string>("name")
            };
        }

        public Task<LocationInfo> GetLocationInfoAsync(long locationId)
        {
            throw new NotImplementedException();
        }

        public Task<BitmapImage> GetProfilePictureAsync(PictureSize pictureSize = PictureSize.Size160x160)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Event>> GetEventsByKeywordAsync(string locationKeyword, uint offset = 0, uint? limit = null)
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEventAsync(string eventId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Event>> GetMyEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Event>> GetHighlightsEventsAsync(DateTime? since, DateTime? until)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostEventAttendanceAsync(string eventId)
        {
            throw new NotImplementedException();
        }
    }
}
