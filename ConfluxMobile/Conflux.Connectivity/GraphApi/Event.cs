using System;

namespace Conflux.Connectivity.GraphApi
{
    public class Event
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string TimeDetails { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Location { get; set; }



    }
}
