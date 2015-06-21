﻿using System.Net.Http;
using System.Threading.Tasks;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;

namespace Conflux.Connectivity
{
    internal class FacebookRequestHandler : IFacebookRequestHandler
    {
        private readonly AccessToken accessToken;

        private readonly HttpClient httpClient;

        public FacebookRequestHandler(AccessToken accessToken)
        {
            this.accessToken = accessToken;
            httpClient = new HttpClient();
        }

        public async Task<string> GetUserNameInfoAsync()
        {
            var response = await httpClient.GetStringAsync(FacebookUriCollection.GetUserNameInfoUri(accessToken));
            return response;
        }

        public async Task<string> GetLocationInfoAsync(long locationId)
        {
            var response = await httpClient.GetStringAsync(FacebookUriCollection.GetLocationIdUri(locationId));
            return response;
        }
        
        public async Task<string> GetProfilePictureAsync(PictureSize pictureSize = PictureSize.Size160x160)
        {
            var response = await httpClient.GetStringAsync(FacebookUriCollection.GetProfilePictureUri(accessToken, pictureSize));
            return response;
        }

        public async Task<string> GetEventsByKeywordAsync(string locationKeyword, uint offset = 0, uint? limit = null)
        {
            var response = await httpClient.GetStringAsync(FacebookUriCollection.GetEventsByLocationKeywordUri(accessToken, locationKeyword, offset, limit));
            return response;
        }

        public async Task<string> GetEventAsync(string eventId)
        {
            var response = await httpClient.GetStringAsync(FacebookUriCollection.GetEventDetailsUri(accessToken, eventId));
            return response;
        }

        public async Task<string> GetMyEvents()
        {
            var response = await httpClient.GetStringAsync(FacebookUriCollection.GetMyEventsUri(accessToken));
            return response;
        }
    }
}
