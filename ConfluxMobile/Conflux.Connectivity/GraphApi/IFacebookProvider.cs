using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Conflux.Connectivity.Authentication;

namespace Conflux.Connectivity.GraphApi
{
    public interface IFacebookProvider
    {
        Task<User> GetUserNameInfoAsync(AccessToken accessToken);

        AuthenticationResult GetAuthenticationResult(string authenticationResponse);

        Task<IEnumerable<Event>> GetEventsByKeywordAsync(AccessToken accessToken, string locationKeyword, uint offset = 0, uint? limit = null);

        Task<BitmapImage> GetProfilePictureAsync(AccessToken accessToken, PictureSize pictureSize = PictureSize.Size160x160);

        Task<Event> GetEventAsync(AccessToken accessToken, string eventId);

        Task<IEnumerable<Event>> GetMyEvents(AccessToken accessToken);
    }
}
