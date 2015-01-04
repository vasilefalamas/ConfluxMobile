using System;

namespace Conflux.Connectivity.GraphApi
{
    public class Event
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public LocationInfo Location { get; set; }
    }
}
