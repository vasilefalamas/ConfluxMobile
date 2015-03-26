using System;
using System.Threading.Tasks;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;

namespace Conflux.Tests
{
    public class FacebookProviderMock : IFacebookProvider
    {
        public async Task<string> GetUserNameInfoAsync(AccessToken accessToken)
        {
            const string responseString = @"{
	                                         ""id"" : ""123456"",
	                                         ""first_name"" : ""John"",
	                                         ""gender"" : ""male"",
	                                         ""last_name"" : ""Doe"",
	                                         ""link"" : ""https://www.facebook.com/app_scoped_user_id/123456/"",
	                                         ""location"" :
	                                         {
		                                         ""id"" : ""106314962738289"",
		                                         ""name"" : ""Sibiu, Romania""
	                                         },
	                                         ""locale"" : ""ro_RO"",
	                                         ""name"" : ""John Doe"",
	                                         ""timezone"" : 2,
	                                         ""updated_time"" : ""2015-03-11T19:01:02+0000"",
	                                         ""verified"" : true
                                        }";

            return responseString;
        }

        public Task<string> GetLocationInfoAsync(long locationId)
        {
            throw new NotImplementedException();
        }

        public AuthenticationResult GetAuthenticationResult(string authenticationResponse)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetProfilePictureAsync(AccessToken accessToken, PictureSize pictureSize = PictureSize.Size160x160)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEventsByKeywordAsync(AccessToken accessToken, string locationKeyword, uint offset = 0, uint? limit = null)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEventAsync(AccessToken accessToken, string eventId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetMyEvents(AccessToken accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
