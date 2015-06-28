using System.Collections.Generic;
using Conflux.Core.Models;

namespace Conflux.UI.Models
{
    public class EventsGroup : List<EventDisplayItem>
    {
        public string Key { get; set; }

        public new IEnumerator<EventDisplayItem> GetEnumerator()
        {
            return base.GetEnumerator();
        } 
    }
}
