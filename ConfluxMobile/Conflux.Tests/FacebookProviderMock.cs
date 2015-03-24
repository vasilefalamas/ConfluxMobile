using System;
using System.Threading.Tasks;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;

namespace Conflux.Tests
{
    public class FacebookProviderMock : IFacebookProvider
    {
        public Task<string> GetUserNameInfoAsync(AccessToken accessToken)
        {
            throw new NotImplementedException();
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
