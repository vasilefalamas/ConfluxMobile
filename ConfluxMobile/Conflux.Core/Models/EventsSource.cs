using System.Collections.Generic;
using System.Threading.Tasks;
using Conflux.Connectivity.Authentication;
using Conflux.Connectivity.GraphApi;

namespace Conflux.Core.Models
{
    public class EventsSource : IIncrementalSource<Event>
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

        public async Task<IEnumerable<Event>> GetPagedItems(uint pageIndex, uint pageSize)
        {
            var results = await facebookProvider.GetEventsByKeywordAsync(accessToken, currentLocationKeyword, pageIndex, pageSize);

            return results;
        }
    }
}
