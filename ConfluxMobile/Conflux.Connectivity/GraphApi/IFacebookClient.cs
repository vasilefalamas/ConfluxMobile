using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Conflux.Connectivity.GraphApi
{
    public interface IFacebookClient
    {
        Task<User> GetUserNameInfoAsync();

        Task<LocationInfo> GetLocationInfoAsync(long locationId);

        Task<BitmapImage> GetProfilePictureAsync(PictureSize pictureSize = PictureSize.Size160x160);

        Task<IEnumerable<Event>> GetEventsByKeywordAsync(string locationKeyword, uint offset = 0, uint? limit = null);

        Task<Event> GetEventAsync(string eventId);

        Task<IEnumerable<Event>> GetMyEvents();

        Task<bool> PostEventAttendance(string eventId);
    }
}
