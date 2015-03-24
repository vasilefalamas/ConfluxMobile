using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conflux.Connectivity;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;

namespace Conflux.Core.Models
{
    public class EventsSource : IIncrementalSource<EventDisplayItem>
    {
        private readonly FacebookDataAccess facebookDataAccess;

        private readonly AccessToken accessToken;

        private readonly string currentLocationKeyword;

        public EventsSource(IFacebookProvider facebookProvider, AccessToken accessToken, string currentLocationKeyword)
        {
            facebookDataAccess = new FacebookDataAccess(facebookProvider);

            this.accessToken = accessToken;
            this.currentLocationKeyword = currentLocationKeyword;
        }

        public async Task<IEnumerable<EventDisplayItem>> GetPagedItems(uint offset, uint limit)
        {
            var pagedResult = await facebookDataAccess.GetEventsByKeywordAsync(accessToken, currentLocationKeyword, offset, limit);

            var events = pagedResult.Select(item => new EventDisplayItem
            {
                Event = item
            }); 

            return events;
        }
    }
}
