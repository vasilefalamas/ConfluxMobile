using System.Net.Http;
using System.Threading.Tasks;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;

namespace Conflux.Connectivity
{
    public class FacebookProvider : IFacebookProvider
    {
        private readonly HttpClient httpClient;

        public FacebookProvider()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> GetUserNameInfoAsync(AccessToken accessToken)
        {
            var response = await httpClient.GetStringAsync(FacebookUriProvider.GetUserNameInfoUri(accessToken));
            return response;
        }

        public async Task<string> GetLocationInfoAsync(long locationId)
        {
            var response = await httpClient.GetStringAsync(FacebookUriProvider.GetLocationIdUri(locationId));
            return response;
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


        public async Task<string> GetProfilePictureAsync(AccessToken accessToken, PictureSize pictureSize = PictureSize.Size160x160)
        {
            var response = await httpClient.GetStringAsync(FacebookUriProvider.GetProfilePictureUri(accessToken, pictureSize));
            return response;
        }

        public async Task<string> GetEventsByKeywordAsync(AccessToken accessToken, string locationKeyword, uint offset = 0, uint? limit = null)
        {
            var response = await httpClient.GetStringAsync(FacebookUriProvider.GetEventsByLocationKeywordUri(accessToken, locationKeyword, offset, limit));
            return response;
        }

        public async Task<string> GetEventAsync(AccessToken accessToken, string eventId)
        {
            var response = await httpClient.GetStringAsync(FacebookUriProvider.GetEventDetailsUri(accessToken, eventId));
            return response;
        }

        public async Task<string> GetMyEvents(AccessToken accessToken)
        {
            var response = await httpClient.GetStringAsync(FacebookUriProvider.GetMyEvents(accessToken));
            return response;
        }
    }
}
