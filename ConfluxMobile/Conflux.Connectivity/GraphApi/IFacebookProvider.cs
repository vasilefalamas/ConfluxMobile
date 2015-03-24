using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Conflux.Connectivity.Authentication;

namespace Conflux.Connectivity.GraphApi
{
    public interface IFacebookProvider
    {
        Task<string> GetUserNameInfoAsync(AccessToken accessToken);

        Task<string> GetLocationInfoAsync(long locationId);

        AuthenticationResult GetAuthenticationResult(string authenticationResponse);

        Task<string> GetProfilePictureAsync(AccessToken accessToken, PictureSize pictureSize = PictureSize.Size160x160);

        Task<string> GetEventsByKeywordAsync(AccessToken accessToken, string locationKeyword, uint offset = 0, uint? limit = null);

        Task<string> GetEventAsync(AccessToken accessToken, string eventId);

        Task<string> GetMyEvents(AccessToken accessToken);
    }
}
