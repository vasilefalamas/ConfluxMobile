using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conflux.Connectivity.GraphApi;

namespace Conflux.Core.Models
{
    public class NewestEventsSource : IIncrementalSource<EventDisplayItem>
    {
        private readonly IFacebookClient facebookClient;
        
        private readonly string currentLocationKeyword;

        public NewestEventsSource(IFacebookClient facebookClient, string currentLocationKeyword)
        {
            this.facebookClient = facebookClient;
            this.currentLocationKeyword = currentLocationKeyword;
        }

        public async Task<IEnumerable<EventDisplayItem>> GetPagedItems(uint offset, uint limit)
        {
            var pagedResult = await facebookClient.GetEventsByKeywordAsync(currentLocationKeyword, offset, limit);

            var events = pagedResult.Select(item => new EventDisplayItem
            {
                Event = item
            }); 

            return events;
        }
    }
}
