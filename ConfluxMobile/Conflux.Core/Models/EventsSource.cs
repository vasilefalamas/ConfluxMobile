using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;

namespace Conflux.Core.Models
{
    public class EventsSource : IIncrementalSource<EventDisplayItem>
    {
        private readonly IFacebookProvider facebookProvider;

        private readonly AccessToken accessToken;

        private readonly string currentLocationKeyword;

        public EventsSource(IFacebookProvider facebookProvider, AccessToken accessToken, string currentLocationKeyword)
        {
            this.facebookProvider = facebookProvider;
            this.accessToken = accessToken;
            this.currentLocationKeyword = currentLocationKeyword;
        }

        public async Task<IEnumerable<EventDisplayItem>> GetPagedItems(uint offset, uint limit)
        {
            var pagedResult = await facebookProvider.GetEventsByKeywordAsync(accessToken, currentLocationKeyword, offset, limit);

            var events = pagedResult.Select(item => new EventDisplayItem
            {
                Event = item
            }); 

            return events;
        }
    }
}
